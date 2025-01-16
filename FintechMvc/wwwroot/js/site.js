// site.js

$(document).ready(function () {
    $('#createAccountButton').on('click', function () {
        $('#createAccountModal').modal('show');
    });

    $('#createAccountForm').on('submit', function (e) {
        e.preventDefault();

        // Limpiar errores previos
        $('.text-danger').text('');

        var formData = {
            AccountNumber: $('#AccountNumber').val(),
            Balance: $('#Balance').val(),
            AccountType: $('#AccountType').val()
        };

        
        $.ajax({
            url: '/Cuentas/Create',
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('#message').html('<div class="alert alert-success mt-3">Cuenta creada con éxito.</div>');

                    $('#createAccountModal').modal('hide');

                    location.reload(); 
                } else {
                    if (response.errors) {
                        if (response.errors.AccountNumber) {
                            $('#AccountNumberError').text(response.errors.AccountNumber);
                        }
                        if (response.errors.Balance) {
                            $('#BalanceError').text(response.errors.Balance);
                        }
                        if (response.errors.AccountType) {
                            $('#AccountTypeError').text(response.errors.AccountType);
                        }
                    }
                }
            },
            error: function (xhr, status, error) {
                $('#message').html('<div class="alert alert-danger mt-3">Hubo un error al procesar la solicitud. Inténtalo nuevamente.</div>');
            }
        });
    });
});
