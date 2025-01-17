$(document).ready(function () {
    
    $(".btn-delete").click(function () {
        var accountId = $(this).data("id");

        if (confirm("¿Estás seguro de que deseas eliminar esta cuenta?")) {
            
            $.ajax({
                url: 'http://localhost:5036/api/Cuentas/'+accountId,
                type: 'DELETE',
                data: { id: accountId },  
                success: function (response) {
                    if (response.success) {
                        $("#row-" + accountId).remove();
                    } else {
                        alert(response.message); 
                    }
                    window.location.reload();
                },
                error: function (xhr, status, error) {
                    // Si hay un error en la solicitud AJAX
                    if (xhr.status === 404) {
                        alert("La cuenta no se encuentra.");
                    } else if (xhr.status === 500) {
                        alert("Hubo un error al intentar eliminar la cuenta.");
                    } else {
                        alert("Error desconocido: " + error);
                    }
                }
            });
        }
    });

    $(".btn-update").click(function () {
        var accountId = $(this).data("id");

        // Abrir el formulario de edición
        $("#editModal").show();

        // Obtener los datos actuales de la cuenta
        var row = $("#row-" + accountId);
        var accountNumber = row.find("td").eq(1).text();
        var balance = row.find("td").eq(2).text().replace("$", "");
        var accountType = row.find("td").eq(3).text();

        // Rellenar el formulario con los valores actuales
        $("#accountId").val(accountId);
        $("#accountNumber").val(accountNumber);
        $("#balance").val(balance);
        $("#accountType").val(accountType);
    });

    // Acción para enviar los cambios
    $("#editForm").submit(function (e) {
        e.preventDefault(); // Evitar el comportamiento por defecto del formulario

        var accountId = $("#accountId").val();
        var updatedAccount = {
            id: parseInt(accountId),
            accountNumber: $("#accountNumber").val(),
            balance: parseInt($("#balance").val()),
            accountType: parseInt($("#accountType").val())
        };
        
        var row = $("#row-" + accountId);
        console.log(updatedAccount);
        console.log(row.find("td").eq(1).text());

       
        $.ajax({
            url: 'http://localhost:5036/api/Cuentas/' + accountId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(updatedAccount),
            success: function (response) {

                console.log(row.find("td").eq(1).text(updatedAccount.accountNumber));
                    row.find("td").eq(1).text(updatedAccount.accountNumber);
                    row.find("td").eq(2).text("$" + updatedAccount.balance);
                    row.find("td").eq(3).text(updatedAccount.accountType);
                    
                    // $("#editModal").hide();
                    //alert("Cuenta actualizada exitosamente.");
                
            },
            error: function () {
                alert("Error al actualizar la cuenta.");
            }
        });
    });
    
    $("#cancelEdit").click(function () {
        
        $("#editModal").hide();
    });
});

$(document).ready(function () {
    
    var contador = 0;

   
    $(".btn-sumar").click(function () {
      
        contador += 1;

     
        $("#contador").text(contador);

       
        console.log("Valor actual del contador: " + contador);
    });
});
