
var DeleteTaxCalculation = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "DELETE",
                url: "Home/DeleteConfirmed/" + id,
                success: function (result) {
                    console.log(result);
                    var message = "Tax Calaculation has been deleted successfully.";
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            location.reload(true);
                        }
                    });
                }
            });
        }
    });
};