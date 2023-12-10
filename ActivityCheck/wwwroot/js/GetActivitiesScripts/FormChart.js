$(function () {
    $("#GetChart").click(function () {
        $.ajax({
            type: "POST",
            url: "/Activity/GetAllActivitiesForChart",
            data: "",
            contextType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSucsessResult,
            error: OnError
        });
    });

        function OnSucsessResult(data) {
            var chartName = "ActivityChart";
            var _data = data;
            var _chartLabels = _data[0];
            var _chartData = _data[1];

            new Chart(chartName, {
                type: "bar",
                data: {
                    labels: _chartLabels,
                    datasets: [{
                        data: _chartData
                    }]
                }, options: {
                    onClick: graphClickEvent
                }
            });
        }

    function graphClickEvent(event, array) {
        //var props = Object.keys(event.chart.config._config.data.labels[array[0].index]);
        //alert(event.chart.config._config.data.labels[array[0].index]);
        if (true) {
            window.location.href = `/Activity/GetActivitiesByDate?date=${event.chart.config._config.data.labels[array[0].index]}`;
        }
    }

        function OnError(err) {

        }
});