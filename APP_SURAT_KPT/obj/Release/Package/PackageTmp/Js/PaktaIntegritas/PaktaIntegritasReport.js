$(document).ready(function () {
    $(function () {
        $('#report-tbl').DataTable({
            columnDefs: [
                {
                    targets: [0, 1, 2, 3, 4],
                    orderable: true,
                    width: "5%",
                    className: 'text-center'
                }
            ],
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
})