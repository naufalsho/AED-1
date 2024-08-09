function pieOpt() {
    return {
        series: [],
        labels: [],
        colors: [],
        dataLabels: {
            formatter: function (val, opts) {
                return opts.w.config.series[opts.seriesIndex]
            }
        },
        chart: {
            type: 'pie',
        },
        legend: {
            show: true,
            position: 'bottom'
        },
        plotOptions: {
            pie: {
                expandOnClick: true
            }
        }
    }
}

function getStatusColor(status) {
    switch (status) {
        case assetStatus.Available:
            return '#00E396' // Hijau
            break;
        case assetStatus.OnUser:
            return '#008FFB' // Biru tua
            break;
        case assetStatus.OnRepair:
            return '#FEB019' // Oren tua
            break;
        case assetStatus.EndOfPeriod:
            return '#222222' // Hitam
            break;
        case assetStatus.Purchased:
            return '#5AC8FA' // Biru muda
            break;
        case assetStatus.BackToVendor:
            return '#BBE244' // Lime
            break;
        case assetStatus.AssetLost:
            return '#FA1B3C' // Merah
            break;
        default:
            return '#FFFFFF'
            break;
    }
}

function getMonthName(month) {
    const date = new Date();
    date.setMonth(month - 1);

    return date.toLocaleString('en-US', { month: 'short' });
}

!function () {
    deviceStockByStatusChart();
    deviceAllocatedChart();
    deviceStockByCategoryChart();
    deviceStockByBrandChart();
    deviceYoY();
    deviceEndPeriod();
    deviceEndPeriodYear();
}();

function deviceStockByStatusChart() {
    $.getJSON('/dashboard/devicestockstatus').done(function (response) {
        let opt = pieOpt();

        opt.series = [];
        opt.labels = [];
        opt.colors = [];

        let pcData = response.filter(m => m.deviceType === 'PC');
        let totalPc = 0;
        jQuery.each(pcData, function (index, item) {
            totalPc += item.deviceCount;
            opt.series.push(item.deviceCount);
            opt.labels.push(item.deviceStatusUI);
            opt.colors.push(getStatusColor(item.deviceStatus));
        });

        let chartPc = new ApexCharts(document.querySelector("#deviceStockStatusPc"), opt);
        chartPc.render();
        $('#totalStatusPcLbl').text(totalPc);

        opt.series = [];
        opt.labels = [];
        opt.colors = [];

        let nbData = response.filter(m => m.deviceType === 'Notebook');
        let totalNb = 0;
        jQuery.each(nbData, function (index, item) {
            totalNb += item.deviceCount;
            opt.series.push(item.deviceCount);
            opt.labels.push(item.deviceStatusUI);
            opt.colors.push(getStatusColor(item.deviceStatus));
        });

        let chartNb = new ApexCharts(document.querySelector("#deviceStockStatusNb"), opt);
        chartNb.render();
        $('#totalStatusNbLbl').text(totalNb);
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    });
}

function deviceAllocatedChart() {
    $.getJSON('/dashboard/deviceallocated').done(function (response) {
        let opt = pieOpt();

        opt.series = [];
        opt.labels = [];
        opt.colors = [];

        let pcData = response.filter(m => m.deviceType === 'PC');
        let totalPc = 0;
        jQuery.each(pcData, function (index, item) {
            totalPc += item.deviceCount;
            opt.series.push(item.deviceCount);
            opt.labels.push(item.branchName);
            opt.colors.push(item.chartColor);
        });

        let chartPc = new ApexCharts(document.querySelector("#deviceAllocatedPc"), opt);
        chartPc.render();
        $('#totalAllocatedPcLbl').text(totalPc);

        opt.series = [];
        opt.labels = [];
        opt.colors = [];

        let nbData = response.filter(m => m.deviceType === 'Notebook');
        let totalNb = 0;
        jQuery.each(nbData, function (index, item) {
            totalNb += item.deviceCount;
            opt.series.push(item.deviceCount);
            opt.labels.push(item.branchName);
            opt.colors.push(item.chartColor);
        });

        let chartNb = new ApexCharts(document.querySelector("#deviceAllocatedNb"), opt);
        chartNb.render();
        $('#totalAllocatedNbLbl').text(totalNb);
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    });
}

function deviceStockByCategoryChart() {
    $.getJSON('/dashboard/devicestockcategory').done(function (response) {
        let deviceType = [];
        let dataOnUser = [];
        let dataAvailable = [];
        let dataOnRepair = [];

        jQuery.each(response, function (index, item) {
            if (item.deviceCategory) {
                deviceType.push(item.deviceCategory);
            } else {
                deviceType.push('');
            }

            if (item.device.filter(m => m.parameter === 'ON_USER')[0]) {
                dataOnUser.push(item.device.filter(m => m.parameter === 'ON_USER')[0].count);
            } else {
                dataOnUser.push(0);
            }

            if (item.device.filter(m => m.parameter === 'AVAILABLE')[0]) {
                dataAvailable.push(item.device.filter(m => m.parameter === 'AVAILABLE')[0].count);
            } else {
                dataAvailable.push(0);
            }

            if (item.device.filter(m => m.parameter === 'ON_REPAIR')[0]) {
                dataOnRepair.push(item.device.filter(m => m.parameter === 'ON_REPAIR')[0].count);
            } else {
                dataOnRepair.push(0);
            }
        });

        let opt = {
            series: [
                {
                    name: 'On User',
                    data: dataOnUser
                },
                {
                    name: 'Available',
                    data: dataAvailable
                },
                {
                    name: 'On Repair',
                    data: dataOnRepair
                }
            ],
            colors: [getStatusColor(assetStatus.OnUser), getStatusColor(assetStatus.Available), getStatusColor(assetStatus.OnRepair)],
            chart: {
                type: 'bar',
                height: 275,
                stacked: true
            },
            legend: {
                show: true,
                showForNullSeries: false,
                showForZeroSeries: false,
                position: 'bottom'
            },
            plotOptions: {
                bar: {
                    borderRadius: 4,
                    horizontal: true,
                    dataLabels: {
                        total: {
                            enabled: true,
                            offsetX: 0,
                            style: {
                                fontSize: '10px',
                                fontWeight: 900
                            }
                        }
                    }
                }
            },
            stroke: {
                width: 1,
                colors: ['#fff']
            },
            xaxis: {
                categories: deviceType
            }
        }

        let chart = new ApexCharts(document.querySelector("#deviceStockCategory"), opt);
        chart.render();
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    });
}

function deviceStockByBrandChart() {
    $.getJSON('/dashboard/devicestockbrand').done(function (response) {
        let opt = pieOpt();

        let pcData = response.filter(m => m.deviceType === 'PC');
        let totalPc = 0;
        jQuery.each(pcData, function (index, item) {
            totalPc += item.deviceCount;
            opt.series.push(item.deviceCount);
            opt.labels.push(item.productBrand);
            opt.colors.push(item.chartColor);
        });

        let chartBrandPc = new ApexCharts(document.querySelector("#deviceStockBrandPc"), opt);
        chartBrandPc.render();
        $('#totalBrandPcLbl').text(totalPc);

        opt.series = [];
        opt.labels = [];

        let nbData = response.filter(m => m.deviceType === 'Notebook');
        let totalNb = 0;
        jQuery.each(nbData, function (index, item) {
            totalNb += item.deviceCount;
            opt.series.push(item.deviceCount);
            opt.labels.push(item.productBrand);
            opt.colors.push(item.chartColor);
        });

        let chartBrandNb = new ApexCharts(document.querySelector("#deviceStockBrandNb"), opt);
        chartBrandNb.render();
        $('#totalBrandNbLbl').text(totalNb);
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    });
}

function deviceYoY() {
    $.getJSON('/dashboard/deviceyoy').done(function (response) {
        console.log(response);
        let year = [];
        let dataPc = [];
        let dataNb = [];

        let minCount = 0;
        let maxCount = 0;

        let nbColor = '';
        let pcColor = '';

        jQuery.each(response, function (index, item) {
            if (item.year) {
                year.push(item.year);
            } else {
                year.push('');
            }

            if (item.device.filter(m => m.parameter === 'PC')[0]) {
                let dataCount = item.device.filter(m => m.parameter === 'PC')[0];

                dataPc.push(dataCount.count);

                if (dataCount.count > maxCount) {
                    maxCount = dataCount.count;
                }

                if (dataCount.count < minCount) {
                    minCount = dataCount.count;
                }

                if (!pcColor) {
                    pcColor = dataCount.chartColor;
                }
            } else {
                dataPc.push(0);
            }

            if (item.device.filter(m => m.parameter === 'Notebook')[0]) {
                let dataCount = item.device.filter(m => m.parameter === 'Notebook')[0];

                dataNb.push(dataCount.count);

                if (dataCount.count > maxCount) {
                    maxCount = dataCount.count;
                }

                if (dataCount.count < minCount) {
                    minCount = dataCount.count;
                }

                if (!nbColor) {
                    nbColor = dataCount.chartColor;
                }
            } else {
                dataNb.push(0);
            }
        });

        let opt = {
            series: [
                {
                    name: 'Notebook',
                    data: dataNb
                },
                {
                    name: 'PC',
                    data: dataPc
                }
            ],
            chart: {
                type: 'line',
                height: 275,
                dropShadow: {
                    enabled: true,
                    color: '#000',
                    top: 18,
                    left: 7,
                    blur: 10,
                    opacity: 0.2
                },
                toolbar: {
                    show: false
                }
            },
            colors: [nbColor, pcColor],
            dataLabels: {
                enabled: true,
                style: {
                    fontSize: '10px',
                    fontFamily: 'Helvetica, Arial, sans-serif',
                    fontWeight: 'bold',
                    colors: ['#222222']
                }
            },
            stroke: {
                width: 1
            },
            xaxis: {
                categories: year
            },
            yaxis: {
                min: Math.floor(minCount / 100) * 100,
                max: Math.ceil(maxCount / 100) * 100
            },
            legend: {
                show: true,
                showForNullSeries: false,
                showForZeroSeries: false,
                position: 'bottom'
            }
        }

        let chart = new ApexCharts(document.querySelector("#deviceYoY"), opt);
        chart.render();
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    });
}

function deviceEndPeriod() {
    $.getJSON('/dashboard/deviceendperiod').done(function (response) {
        if (response.length > 0) {
            let branch = [];
            let dataPc = [];
            let dataNb = [];

            let nbColor = '';
            let pcColor = '';

            jQuery.each(response, function (index, item) {
                if (item.branchName) {
                    branch.push(item.branchName);
                } else {
                    branch.push('');
                }

                if (item.device.filter(m => m.parameter === 'PC')[0]) {
                    let dataCount = item.device.filter(m => m.parameter === 'PC')[0];
                    dataPc.push(dataCount.count);

                    if (!pcColor) {
                        pcColor = dataCount.chartColor;
                    }
                } else {
                    dataPc.push(0);
                }

                if (item.device.filter(m => m.parameter === 'Notebook')[0]) {
                    let dataCount = item.device.filter(m => m.parameter === 'Notebook')[0];
                    dataNb.push(dataCount.count);

                    if (!nbColor) {
                        nbColor = dataCount.chartColor;
                    }
                } else {
                    dataNb.push(0);
                }
            });

            let opt = {
                series: [
                    {
                        name: 'Notebook',
                        data: dataNb,
                    },
                    {
                        name: 'PC',
                        data: dataPc
                    }
                ],
                chart: {
                    type: 'bar',
                    height: 275
                },
                legend: {
                    show: true,
                    showForNullSeries: false,
                    showForZeroSeries: false,
                    position: 'bottom'
                },
                colors: [nbColor, pcColor],
                plotOptions: {
                    bar: {
                        borderRadius: 4,
                        horizontal: false,
                        dataLabels: {
                            position: 'top'
                        }
                    }
                },
                xaxis: {
                    categories: branch
                },
                dataLabels: {
                    enabled: true,
                    offsetY: -20,
                    style: {
                        fontSize: '10px',
                        fontFamily: 'Helvetica, Arial, sans-serif',
                        fontWeight: 'bold',
                        colors: ['#222222']
                    }
                }
            }

            let chart = new ApexCharts(document.querySelector("#deviceEndPeriod"), opt);
            chart.render();
        } else {
            $('#colEndPeriod').hide();
        }
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    });
}

function deviceEndPeriodYear() {
    $.getJSON('/dashboard/deviceendperiodyear').done(function (response) {
        if (response.length > 0) {
            let month = [];
            let dataPc = [];
            let dataNb = [];

            let nbColor = '';
            let pcColor = '';

            jQuery.each(response, function (index, item) {
                if (item.month) {
                    month.push(getMonthName(item.month));
                } else {
                    month.push('');
                }

                if (item.device.filter(m => m.parameter === 'PC')[0]) {
                    let dataCount = item.device.filter(m => m.parameter === 'PC')[0];
                    dataPc.push(dataCount.count);

                    if (!pcColor) {
                        pcColor = dataCount.chartColor;
                    }
                } else {
                    dataPc.push(0);
                }

                if (item.device.filter(m => m.parameter === 'Notebook')[0]) {
                    let dataCount = item.device.filter(m => m.parameter === 'Notebook')[0];
                    dataNb.push(dataCount.count);

                    if (!nbColor) {
                        nbColor = dataCount.chartColor;
                    }
                } else {
                    dataNb.push(0);
                }
            });

            let opt = {
                series: [
                    {
                        name: 'Notebook',
                        data: dataNb
                    },
                    {
                        name: 'PC',
                        data: dataPc
                    }
                ],
                chart: {
                    type: 'bar',
                    height: 275
                },
                legend: {
                    show: true,
                    showForNullSeries: false,
                    showForZeroSeries: false,
                    position: 'bottom'
                },
                colors: [nbColor, pcColor],
                plotOptions: {
                    bar: {
                        borderRadius: 4,
                        horizontal: false,
                        dataLabels: {
                            position: 'top'
                        }
                    }
                },
                xaxis: {
                    categories: month
                },
                dataLabels: {
                    enabled: true,
                    offsetY: -20,
                    style: {
                        fontSize: '10px',
                        fontFamily: 'Helvetica, Arial, sans-serif',
                        fontWeight: 'bold',
                        colors: ['#222222']
                    },
                    formatter: function (val) {
                        if (val != 0) {
                            return val;
                        }
                    }
                }
            }

            let chart = new ApexCharts(document.querySelector("#deviceEndPeriodYear"), opt);
            chart.render();
        } else {
            $('#colEndPeriodYear').hide();
        }
    }).fail(function (response) {
        HandleHttpRequestFail(response);
    });
}
