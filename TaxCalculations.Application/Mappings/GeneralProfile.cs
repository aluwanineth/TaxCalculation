using AutoMapper;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetAllTaxCalculation;
using TaxCalculations.Application.Features.TaxCalculate.Queries.GetTaxCalculation;
using TaxCalculations.Domain.Entities;

namespace TaxCalculations.Application.Mappings;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<TaxCalculation, TaxCalculateQueryResponse>().ReverseMap();
        CreateMap<TaxCalculation, GetTaxCalculationByIdQueryResponse>().ReverseMap(); 
    }
}