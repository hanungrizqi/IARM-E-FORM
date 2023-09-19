$(document).ready(function () {
    $(function () {
        $('#report-tbl').DataTable({
            columnDefs: [
                {
                    targets: [0],
                    orderable: true,
                    width: "5%",
                    className: 'text-center'
                }
            ],
            scrollX: true,
            processing: true,
            pagingType: "full_numbers",
            pageLength: 10,
            lengthMenu: [10, 25, 50],
            ajax: {
                url: `${apiUrl}api/integritas/datatable`,
                dataSrc: 'Data'
            },
            columns: [
                {
                    data: null,
                    render: (data, type, row, meta) => {
                        return ++meta.row
                    }
                },
                {
                    data: 'NRP',
                    render: (data, type, row, meta) => {
                        return data
                    }
                },
                {
                    data: 'NAME',
                    render: (data, type, row, meta) => {
                        return data
                    }
                },
                {
                    data: 'SUBMIT',
                    render: (data, type, row, meta) => {
                        if (data == true) {
                            return `<span class="label label-success">Sudah</span>`
                        }
                        else {
                            return `<span class="label label-danger">Belum</span>`
                        }
                    }
                },
                {
                    data: 'DEPT',
                    render: (data, type, row, meta) => {
                        return data
                    }
                },
                {
                    data: 'SITE',
                    render: (data, type, row, meta) => {
                        return data
                    }
                },
                {
                    data: 'SIGN_LOCATION',
                    render: (data, type, row, meta) => {
                        return data
                    }
                },
                {
                    data: 'SUBMITDATE',
                    render: (data, type, row, meta) => {
                        if (data != null) {
                            return data.slice(0, 10)
                        }
                        else {
                            return "Tidak ada data"
                        }
                        
                    }
                }
            ]
        })
    })

    // init chart
    $(function () {
        var district = document.querySelector('#chart-district').value
        var dept = document.querySelector('#chart-dept').value

        var dataRequest = {
            District: district,
            Dept: dept
        }

        $.ajax({
            url: `${apiUrl}api/integritas/chart`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(dataRequest)
        }).done(function (response) {
            console.log(response.Data)
            var contex = document.getElementById('chartPI')
            chartJs = new Chart(contex, {
                type: 'bar',
                data: response.Data,
                options: {
                    scales: {
                        x: {
                            stacked: true
                        },
                        y: {
                            beginAtZero: true
                        }
                    },
                    responsive: true,
                    interaction: {
                        intersect: false,
                        mode: 'index',
                    },
                }
            });
        }).fail(function (jqXHR, textStatus) {
            console.log(jqXHR, textStatus);
        });
    })

    $('.select2').select2({
        placeholder: "Pilih input",
        allowClear: true
    });

    function getDistrict() {
        var request = $.ajax({
            url: `${apiUrl}api/masterdata/district`,
            method: "GET",
        }).done(function (response) {
            response.Data.forEach((val, idx) => {
                $('.list-district').each(function () {
                    $(this).append($('<option/>', {
                        value: val.DSTRCT_CODE,
                        text: val.DSTRCT_CODE
                    }));
                });
            });
        }).fail(function (jqXHR, textStatus) {
            console.log(jqXHR, textStatus);
        });
    }

    function getDept() {
        var request = $.ajax({
            url: `${apiUrl}api/masterdata/dept`,
            method: "GET",
        }).done(function (response) {
            response.Data.forEach((val, idx) => {
                $('.list-dept').each(function () {
                    $(this).append($('<option/>', {
                        value: val.DEPT_NAME,
                        text: val.DEPT_NAME
                    }));
                });
            });
        }).fail(function (jqXHR, textStatus) {
            console.log(jqXHR, textStatus);
        });
    }

    $("#filter-pi").click(async function () {
        var namaKaryawan = document.querySelector('#filter-name').value
        var district = document.querySelector('#filter-district').value
        var dept = document.querySelector('#filter-dept').value

        var dataRequest = {
            Nama: namaKaryawan,
            District: district,
            Dept: dept
        }

        $.ajax({
            url: `${apiUrl}api/integritas/datatable/filter`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(dataRequest)
        }).done(function (response) {
            $('#report-tbl').DataTable().clear();
            $('#report-tbl').DataTable().rows.add(response.Data)
            $('#report-tbl').DataTable().draw()
        }).fail(function (jqXHR, textStatus) {
            console.log(jqXHR, textStatus);
        });
    });

    $("#chart-pi").click(async function () {
        var district = document.querySelector('#chart-district').value
        var dept = document.querySelector('#chart-dept').value

        var dataRequest = {
            District: district,
            Dept: dept
        }

        $.ajax({
            url: `${apiUrl}api/integritas/chart`,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(dataRequest)
        }).done(function (response) {
            console.log(response.Data)
            var contex = document.getElementById('chartPI')
            chartJs.destroy();
            chartJs = new Chart(contex, {
                type: 'bar',
                data: response.Data,
                options: {
                    scales: {
                        x: {
                            stacked: true
                        },
                        y: {
                            beginAtZero: true
                        }
                    },
                    responsive: true,
                    interaction: {
                        intersect: false,
                        mode: 'index',
                    },
                }
            });
        }).fail(function (jqXHR, textStatus) {
            console.log(jqXHR, textStatus);
        });
    });

    getDept();
    getDistrict();
})