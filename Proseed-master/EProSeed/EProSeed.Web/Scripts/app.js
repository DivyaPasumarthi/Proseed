$(document).ready(function () {

    $('select').select2();
    var createBatchDatesCalendar = $('#createBatchDates').datepicker({
        multidate: true,
        multidateSeparator: ",",
        daysOfWeekHighlighted: "1,2,3,4,5",
        todayHighlight: true
    });

    var editBatchDatesCalendar = $('#ediBatchDates').datepicker({
        multidate: true,
        multidateSeparator: ",",
        daysOfWeekHighlighted: "1,2,3,4,5",
        daysOfWeekDisabled: "0,1,2,3,4,5,6",
        todayHighlight: true,
    });
    var editBatchDates = $('#BatchDates').val();
    if (editBatchDates && editBatchDates.length) {
        var splittedDates = editBatchDates.split(',');
        var batchDates = [];
        for (var i in splittedDates) {
            batchDates.push(new Date(splittedDates[i]));
        }
        $('#ediBatchDates').datepicker('setDates', batchDates);
    }

    function on_date_change(e) {
        var dates = $(this).datepicker('getDates');
        var batchDates = '';
        for (var index in dates) {
            var date = moment(dates[index]).format('MM/DD/YYYY');
            batchDates += date + ',';
        }
        $('#BatchDates').val(batchDates);
    }

    createBatchDatesCalendar.on('changeDate', on_date_change);
    editBatchDatesCalendar.on('changeDate', on_date_change)
    $('[data-toggle="tooltip"]').tooltip();

    Feedback.toggleRow();
    dashboard.ShowBatchName();
    slider.showSlider();
});

var Feedback = {};
Feedback = {
    toggle: function () {
        $('.toggleMyData').click(function () {
            $('.FeedBack_Wrapper').hide();
            var target = $(this).attr('data-target');
            $(target).slideToggle();
        });
    },

    toggleRow: function () {

        $('.toggleMyData').click(function () {
            var target = $(this).attr('data-rowTarget');
            var value = $(this).attr('data-targetVal');
            $(target).slideDown();
            $(target + " td").html(value);

            if ($(this).hasClass('HideFeedback')) {
                $(target).slideUp();

            }

            $(this).toggleClass('HideFeedback');


        });



    }


}


var dashboard = {}
dashboard = {
    ShowBatchName: function () {
        var firstBatch = $('.ShowBatchName');
        if (firstBatch && firstBatch.length) {
            $(firstBatch[0]).parents('tr').addClass('success');
        }
        $('.ShowBatchName').click(function () {
            var target = $(this).text();
            target = target + " Trainees";
            var parents = $(this).parents('tr');
            if (parents && parents.length) {
                parents.addClass('success');
                parents.siblings().removeClass('success');
            }
            $('#DashboardBatchName').html(target);
        });

    }
}

var slider = {}
slider = {
    updateSliderRatingValue: function (connectSlider, element) {
        var formElement = document.getElementById(element);
        var stepValueElement = document.getElementById('slider-' + element + '-value');
        if (stepValueElement) {
            connectSlider.noUiSlider.on('update', function (values, handle) {
                stepValueElement.innerHTML = 'Rating: ' + values[handle];
                formElement.value = Number(values[handle]);
            });
        }
    },
    createSlider: function (element) {
        var connectSlider = document.getElementById('slider-connect-' + element);
        var formElement = document.getElementById(element);
        var rating = 0;
        if (connectSlider) {
            var elementValue = Number(formElement.value);
            if (!isNaN(elementValue)) {
                rating = elementValue;
            }
        }
        if (connectSlider) {
            noUiSlider.create(connectSlider, {
                start: rating,
                connect: [true, false],
                step: 1,
                range: {
                    'min': 0,
                    'max': 10
                },
            });
            slider.updateSliderRatingValue(connectSlider, element);
        }
    },

    showSlider: function () {
        slider.createSlider('PassionForClientSuccess');
        slider.createSlider('FocusOnQuality');
        slider.createSlider('Communication');
        slider.createSlider('Transparency');
        slider.createSlider('Discipline');
        slider.createSlider('Energy');
        slider.createSlider('TeamPlayer');
        slider.createSlider('Commitment');
        slider.createSlider('OwnerShip');
        slider.createSlider('TechnicalCompetency');
        slider.createSlider('TrainerRating');
    }
}
