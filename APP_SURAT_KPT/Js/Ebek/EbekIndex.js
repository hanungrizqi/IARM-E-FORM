$(document).ready(function () {
    var rowTblFamily = 1;
    var rowTblMikad = 1;
    function initUserData() {
        document.getElementById("loadingScreen").style.display = "block";
        var request = $.ajax({
            url: `${apiUrl}api/ebek/getuserdata/${userNrp}`,
            method: "GET",
        }).done(function (response) {
            console.log(response)
            if (response.Success == true) {
                if (response.Data.EBEK_SUBMIT == true) {
                    document.getElementById("form-ebek").style.display = "none";
                    document.getElementById("form-ebek-submit").style.display = "block";
                    document.getElementById("loadingScreen").style.display = "none";
                } else {
                    document.getElementById("form-ebek").style.display = "block";
                    document.getElementById("form-ebek-submit").style.display = "none";

                    document.getElementById("user-name").innerHTML = response.Data.NAME;
                    document.getElementById("sign-name").innerHTML = response.Data.NAME;
                    document.getElementById("user-nrp").innerHTML = response.Data.EMPLOYEE_ID;
                    document.getElementById("user-site").innerHTML = response.Data.DSTRCT_CODE;
                    document.getElementById("user-dept").innerHTML = response.Data.DEPT_DESC;
                    document.getElementById("user-divisi").innerHTML = response.Data.DEPT_DESC;
                    document.getElementById("user-hire-date").innerHTML = new moment(response.Data.HIRE_DATE, "YYYYMMDD").format("YYYY-MMMM-DD") ;
                    document.getElementById("user-gender").innerHTML = response.Data.GENDER_CODE == 'M' ? 'Pria' : 'Wanita';
                    document.getElementById("user-address").innerHTML = response.Data.alamat;
                    document.getElementById("user-phone").innerHTML = response.Data.No_telp;

                    document.getElementById("loadingScreen").style.display = "none";
                }
            } else {
                document.getElementById("form-ebek").style.display = "none";
                document.getElementById("form-ebek-submit").style.display = "block";
                document.getElementById("loadingScreen").style.display = "none";
            }
            
        }).fail(function (jqXHR, textStatus) {
            console.log(jqXHR, textStatus);
        });
    }
    
    $('input[type=checkbox]').change(
        function () {
            switch (this.id) {
                case 'no-family':
                    $("#family").prop('checked', false);
                    $("#family-table").hide()
                    break;
                case 'no-mikad':
                    $("#mikad").prop('checked', false);
                    $("#mikad-table").hide()
                    break;
                case 'no-coi':
                    $("#coi").prop('checked', false);
                    $("#coi-table").hide()
                    break;
                case 'family':
                    $("#no-family").prop('checked', false);
                    $("#family-table").show()
                    break;
                case 'mikad':
                    $("#no-mikad").prop('checked', false);
                    $("#mikad-table").show()
                    break;
                case 'coi':
                    $("#no-coi").prop('checked', false);
                    $("#coi-table").show()
                    break;
                default:
            }
        });

    $(".add-btn").click(function () {
        switch (this.id) {
            case 'family-add-btn':
                var insertRowFamily = `<tr class="row-height">
                            <td class="text-center">${++rowTblFamily}</td>
                            <td class="text-center"><input type="text"></td>
                            <td class="text-center"><input type="text"></td>
                            <td class="text-center"><input type="text"></td>
                            <td class="text-center"><input type="text"></td>
                            <td><button class="btn btn-danger remove-btn">Hapus</button></td>
                        </tr>`
                $("#table-family-body").append(insertRowFamily);
                break;
            case 'mikad-add-btn':
                var insertRowMikad = `<tr class="row-height">
                            <td class="text-center">${++rowTblMikad}</td>
                            <td class="text-center"><input type="text"></td>
                            <td class="text-center"><input type="text"></td>
                            <td class="text-center"><input type="text"></td>
                            <td class="text-center"><input type="text"></td>
                            <td><button class="btn btn-danger remove-btn">Hapus</button></td>
                        </tr>`
                $("#table-mikad-body").append(insertRowMikad);
                break;
            default:
        }
    })

    $('table').on('click', '.remove-btn', function (e) {
        $(this).closest('tr').remove()
    })

    $("#btn-submit-ebek").click(async function () {
        var checkboxCount = $('.user-checkbox').filter(':checked').length;
        var signLocation = $('#sign-location').val();

        if (!signLocation) {
            Swal.fire({
                icon: 'error',
                title: "Lokasi sign harus diisi!",
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("loadingScreen").style.display = "none";
                }
            })
        }
        else if (checkboxCount != 3) {
            Swal.fire({
                icon: 'error',
                title: "Harus ada 3 persetujuan yang harus di ceklist!",
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("loadingScreen").style.display = "none";
                }
            })
        }
        else {
            submitData()
        }
    });

    function submitData() {
        event.preventDefault()

        var listHeaderTable = ['No', 'MemberName', 'MemberPos', 'MemberDept', 'MemberRelation']

        var isHadFamily = $('#family').is(":checked");
        var isHadMikad = $('#mikad').is(":checked");
        var isHadCoi = $('#coi').is(":checked");

        //insert tbl family into list of objects
        var familyTable = document.getElementById('tbl-family');
        var listFamilyData = [];
        if (isHadFamily) {
            for (var i = 1; i < familyTable.rows.length; i++) {
                var row = familyTable.rows[i];
                var familyData = {};
                for (var j = 1; j < (row.cells.length - 1); j++) {
                    var cell = row.cells[j];
                    var header = listHeaderTable[j];
                    familyData[header] = cell.getElementsByTagName('input')[0].value;
                }
                listFamilyData.push(familyData);
            }
            console.log(listFamilyData)
        }
        

        //insert tbl family into list of objects
        var mikadTable = document.getElementById('tbl-mikad');
        var listMikadData = [];
        if (isHadMikad) {
            for (var i = 1; i < mikadTable.rows.length; i++) {
                var row = mikadTable.rows[i];
                var mikadData = {};
                for (var j = 1; j < (row.cells.length - 1); j++) {
                    var cell = row.cells[j];
                    var header = listHeaderTable[j];
                    console.log(cell)
                    mikadData[header] = cell.getElementsByTagName('input')[0].value;
                }
                listMikadData.push(mikadData);
            }
            console.log(listMikadData)
        }

        //insert tbl family into list of objects
        var coiTable = document.getElementById('tbl-coi');
        var listHeaderCoi = ['No', 'Detail', 'MemberCompany', 'MemberPosition', 'Detail']
        var listRowCoi = ['Header', 'BusinessRelation', 'InvestationNonPublic', 'InvestationSupplier', 'AnotherBusiness', 'SideJob']
        var listCoiData = {};
        if (isHadCoi) {
            for (var i = 1; i < coiTable.rows.length; i++) {
                var row = coiTable.rows[i];
                var rowCoi = listRowCoi[i];
                var coiData = {};
                for (var j = 2; j < (row.cells.length); j++) {
                    var cell = row.cells[j];
                    var header = listHeaderCoi[j];
                    console.log(cell)
                    coiData[header] = cell.getElementsByTagName('input')[0].value;
                }
                listCoiData[rowCoi] = coiData;
            }
            console.log(listCoiData)
        }

        const requestData = {
            Nrp: userNrp,
            Location: $('#sign-location').val(),
            NoFamily: $('#no-family').is(":checked"),
            NoMikad: $('#no-mikad').is(":checked"),
            NoCoi: $('#no-coi').is(":checked"),
            HadFamily: listFamilyData,
            HadMikad: listMikadData,
            HadCoi: jQuery.isEmptyObject(listCoiData) ? null : listCoiData
        }

        console.log(requestData)
        Swal.fire({
            icon: 'warning',
            title: "Apakah anda setuju dan data yang diberikan sesuai?",
            showCancelButton: true,
            confirmButtonText: 'Setuju',
            cancelButtonText: 'Tidak',
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-secondary'
            },
            buttonsStyling: false,
        }).then(async (result) => {
            if (result.isConfirmed) {
                document.getElementById("loadingScreen").style.display = "block";
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json",
                    url: apiUrl + "api/ebek/submit",
                    data: JSON.stringify(requestData),
                    success: function (response) {
                        document.getElementById("loadingScreen").style.display = "none";
                        if (response.Success == true) {
                            Swal.fire({
                                icon: 'success',
                                title: response.Message,
                                confirmButtonText: 'Ok'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    window.location.href = '/dashboard/dashboard'
                                }
                            })
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
                                title: response.Message,
                                confirmButtonText: 'Ok'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    
                                }
                            })
                        }
                    }
                })
            }
        })
    }

    initUserData();
})