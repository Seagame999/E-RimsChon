$(document).ready(function () {

    var activityStatus = $.urlParam('status');
    var res = decodeURI(activityStatus);

    var planningCard = $("#planningCard");
    var workingCard = $("#workingCard");
    var finishingCard = $("#finishingCard");

    var planningCheck = $("#planningCheck");
    var workingCheck = $("#workingCheck");
    var finishingCheck = $("#finishingCheck");

    if (activityStatus != null) {

        if (res == 'ยังไม่ดำเนินการ') {
            workingCard.hide();
            finishingCard.hide();

            planningCheck.addClass('inactive');
            workingCheck.addClass('inactive');
            finishingCheck.addClass('inactive');
        } else if (res == 'วางแผน') {
            finishingCard.hide();

            planningCheck.addClass('active');
            workingCheck.addClass('inactive');
            finishingCheck.addClass('inactive');
        } else if (res == 'ดำเนินการ') {
            planningCheck.addClass('active');
            workingCheck.addClass('active');
            finishingCheck.addClass('inactive');
        } else if (res == 'เสร็จสิ้น') {
            planningCheck.addClass('active');
            workingCheck.addClass('active');
            finishingCheck.addClass('active');
        }
    }
});

//get parameters function
$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}