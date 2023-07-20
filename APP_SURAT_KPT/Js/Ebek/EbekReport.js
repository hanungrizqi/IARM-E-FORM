$(document).ready(function () {
    $(function () {
        $('#report-tbl').DataTable({
            columnDefs: [
                {
                    targets: [0],
                    orderable: true,
                    width: "5%",
                    className: 'text-center'
                },
                {
                    targets: [0, 1, 2, 3, 4],
                    className: 'dt-body-center'
                },
            ],
            pagingType: "full_numbers",
            pageLength: 10,
            lengthMenu: [10, 25, 50],
            ajax: {
                url: `${apiUrl}api/ebek/datatable`,
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
                },
                {
                    data: 'ISHADFAMILY',
                    render: (data, type, row, meta) => {
                        if (data == true) {
                            return `<span class="label label-danger">Ada</span>`
                        }
                        else {
                            return `<span class="label label-success">Tidak</span>`
                        }
                    }
                },
                {
                    data: 'ISHADMIKAD',
                    render: (data, type, row, meta) => {
                        if (data == true) {
                            return `<span class="label label-danger">Ada</span>`
                        }
                        else {
                            return `<span class="label label-success">Tidak</span>`
                        }
                    }
                },
                {
                    data: 'ISHADCOI',
                    render: (data, type, row, meta) => {
                        if (data == true) {
                            return `<span class="label label-danger">Ada</span>`
                        }
                        else {
                            return `<span class="label label-success">Tidak</span>`
                        }
                    }
                },
            ]
        })
    })
})