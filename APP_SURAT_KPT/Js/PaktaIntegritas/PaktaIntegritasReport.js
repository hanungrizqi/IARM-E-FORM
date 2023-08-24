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
                $('#list-district').append($('<option/>', {
                    value: val.DSTRCT_CODE,
                    text: val.DSTRCT_CODE
                }));
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
                $('#list-dept').append($('<option/>', {
                    value: val.DEPT_NAME,
                    text: val.DEPT_NAME
                }));
            });
        }).fail(function (jqXHR, textStatus) {
            console.log(jqXHR, textStatus);
        });
    }

    $("#filter-pi").click(async function () {
        var namaKaryawan = document.querySelector('#filter-name').value
        var district = document.querySelector('#list-district').value
        var dept = document.querySelector('#list-dept').value

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

    getDept();
    getDistrict();
})