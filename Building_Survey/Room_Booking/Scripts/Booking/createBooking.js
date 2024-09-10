$(document).ready(function () {
    $("#submitBooking").on("click", function () {
        var errorMessage = validateDateCheckInOut();
        if (errorMessage.length > 0) {
            $("#errorMessage").show();
            $("#errorMessage").html(errorMessage);
            
        }
        return errorMessage.length==0;
    });
    function validateDateCheckInOut()
    {
        var errorMessage = "";
        var chkInDate =  $("#ckInDateTb").val();
        var chkOutDate = $("#ckOutDateTb").val();
        var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
        var greaterThanToday = Date.parse(chkInDate) >= Date.parse(today);
        var greaterThanCheckIn = Date.parse(chkInDate) < Date.parse(chkOutDate);
        if (!greaterThanToday) errorMessage += "Check In date must be greater than today date";
        if (!greaterThanCheckIn) errorMessage += "<br>Check In date must be before check out date";
        return errorMessage;
    }
});