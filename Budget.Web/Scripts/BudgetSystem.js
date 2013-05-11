$(function() {
    $("option[data-is-budget-exist=true]").attr("disabled", "disabled").addClass("disabled").append("(Вже існує)");
});

$(function () {
    $("input[data-checkbox-first-level]").css("color", "red");
    $("input[data-checkbox-first-level]").change(function() {
        var levelName = $(this).attr("data-checkbox-first-level");
        var lowLevelElements = $("input[data-checkbox-second-level=" + levelName + "]");

        if ($(this).prop("checked")) {
            lowLevelElements.each(function () {
                $(this).removeProp("disabled");
            });
            return;
        }
        
        lowLevelElements.each(function () {
            $(this).prop("disabled", true);
            $(this).removeProp("checked");
        });
    });
})