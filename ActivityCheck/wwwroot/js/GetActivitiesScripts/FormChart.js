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
            console.log(_chartData);
            console.log("1");

            new Chart(chartName, {
                type: "bar",
                data: {
                    labels: _chartLabels,
                    datasets: _chartData
                },
                options: {
                    responsive: true,
                    onClick: graphClickEvent,
                    scales: {
                        x: {
                            stacked: true,
                        },
                        y: {
                            stacked: true
                        }
                    }
                }
            });
        }

    function graphClickEvent(event, array) {
            window.location.href = `/Activity/GetActivitiesByDate?date=${event.chart.config._config.data.labels[array[0].index]}`;
    }

        function OnError(err) {
            console.log(err);
        }
});