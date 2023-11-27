using AutoMapper;
using TaxCalculations.Application.Contracts.Repositories;
using TaxCalculations.Application.Enums;
using TaxCalculations.Application.Exceptions;
using TaxCalculations.Application.Features.TaxCalculate.Delete;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetAllTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculation.TaxCalculator.Commands;
using TaxCalculations.Application.Services.TaxCalculator;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Business.TaxCalculator;

public class TaxCalculatorBusinessAsync : ITaxCalculatorBusinessAsync
{
    private readonly ITaxCalculationRepositoryAsync _taxCalculationRepositoryAsync;
    private readonly ITaxCalculatorTypeRepositoryAsync _taxCalculatorTypeRepositoryAsync;
    private readonly ITaxCalculatorService _taxCalculatorService;

    private readonly IMapper _mapper;
    public TaxCalculatorBusinessAsync(ITaxCalculationRepositoryAsync taxCalculationRepositoryAsync,
        ITaxCalculatorTypeRepositoryAsync taxCalculatorTypeRepositoryAsync,
        ITaxCalculatorService taxCalculatorService,
        IMapper mapper
       )
    {
        _taxCalculationRepositoryAsync = taxCalculationRepositoryAsync;
        _taxCalculatorTypeRepositoryAsync = taxCalculatorTypeRepositoryAsync;
        _taxCalculatorService = taxCalculatorService;
        _mapper = mapper;
    }    
    public async Task<Response<TaxCalculateCommandResponse>> CalculateTax(TaxCalculateCommand request)
    {
        var taxType = await _taxCalculatorTypeRepositoryAsync.GetTaxCalculatorTypeAsync(request.PostalCode);
        var taxTypeEnum = Enum.Parse<CalculationTaxType>(taxType);

        var tax = _taxCalculatorService.CalculateTax(taxTypeEnum, request.AnnualIncome);

        await _taxCalculationRepositoryAsync.AddAsync(new Domain.Entities.TaxCalculation
        {
            Id = Guid.NewGuid(),
            PostalCode = request.PostalCode,
            AnnualIncome = request.AnnualIncome,
            TaxCalculationAmount = tax
         });
        
        return Results(tax,request.PostalCode,taxType, request.AnnualIncome);   
    }

    public async Task<Response<Guid>> DeleteTaxCalculateAync(DeleteTaxCalculationByIdCommand request)
    {
        var taxCalculation = await _taxCalculationRepositoryAsync.GetByIdAsync(request.Id);
        if (taxCalculation == null)
            throw new ApiException("Tax Calaculation not found.");
        await _taxCalculationRepositoryAsync.DeleteAsync(taxCalculation);
        return new Response<Guid>(taxCalculation.Id, $"{taxCalculation.Id} Tax calculation deleted successfully");
    }

    public async Task<Response<GetTaxCalculationByIdQueryResponse>> GetTaxCalculateAync(GetTaxCalculationByIdQuery request)
    {
        var taxCalculation = await _taxCalculationRepositoryAsync.GetByIdAsync(request.Id);
        if (taxCalculation == null) throw new ApiException($"Tax Calaculation not found.");
        var results = _mapper.Map<GetTaxCalculationByIdQueryResponse>(taxCalculation);
        return new Response<GetTaxCalculationByIdQueryResponse>(new GetTaxCalculationByIdQueryResponse 
                    {
            Id = request.Id,
            AnnualIncome = taxCalculation.AnnualIncome,
            Created = taxCalculation.Created,   
            PostalCode = taxCalculation.PostalCode,
            TaxCalculationAmount = taxCalculation.TaxCalculationAmount
        });
    }

    public async Task<Response<IEnumerable<TaxCalculateQueryResponse>>> GetTaxCalculations(GetAllTaxCalculationsQuery request)
    {
        var taxCalculations = await _taxCalculationRepositoryAsync.GetAllAsync();
        var orderedQueryResults = taxCalculations.ToList().OrderByDescending(C => C.Created);
        var results = _mapper.Map<IEnumerable<TaxCalculateQueryResponse>>(orderedQueryResults);
        return new Response<IEnumerable<TaxCalculateQueryResponse>>(results);
    }

    private Response<TaxCalculateCommandResponse> Results(double tax, string postalCode, string taxType, double annualIncome)
    {
        return new Response<TaxCalculateCommandResponse>(new TaxCalculateCommandResponse
        {
            PostalCode = postalCode,
            TaxCalculationType = taxType,
            TaxCalculationAmount = tax,
            AnnualIncome =annualIncome
        }, message: $"Tax of type {taxType} calculated Successfully. Calculated tax amount: {tax}");
    }
}
