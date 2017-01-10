


$(document).ready(function () {
    $('#BatchId').change(function () {
        var batchId = $(this).val();
        $("#reportPartial").html("")
        var html = "<option value=''>Select Trainee</option>";
        $('#InducteeId').html(html)
        if (batchId != "") {
            $.ajax({
                url: "/Report/GetIndcutees",
                type: "POST",
                data: { batchId: batchId },
                dataType: "json",
                success: function (data) {
                    for (var i in data) {
                        if (data.hasOwnProperty(i)) {
                            html += "<option value='" + data[i].Id + "'>" + data[i].Name + "</option>";
                        }
                        
                    }
                    $('#InducteeId').html(html);
                }
            });
        }
    });

    $('#InducteeId').change(function () {
        var batchId = $('#BatchId :selected').val();
        var inducteeId = $('#InducteeId :selected').val()
        $("#reportPartial").html("");
        var url = "/Report/ViewReport/";
        if (batchId != "" && inducteeId != "") {
            $.ajax({
                url: url,
                type: "GET",
                data: { batchId: batchId, inducteeId: inducteeId },
                dataType: "html",
                success: function (data) {
                    $("#reportPartial").html(data);
                }
            });
        }
    });
});