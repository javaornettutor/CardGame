$(document).ready(function () {
    $("#curCustSelect").change(function () {
        var curId = $(this).val();
        
        $.ajax({
            type:"GET",
            url: '/ConfirmClientBookings/Index/',
            data: { 'customerId': curId},
            success: function (data) {
                //window.location.reload(); 
            }
        });
    });
});
