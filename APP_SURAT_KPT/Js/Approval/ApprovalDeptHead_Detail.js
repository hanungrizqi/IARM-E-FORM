﻿$("document").ready(function () {
    getDetail();
})

function getDetail() {
    $.ajax({
        url: apiUrl + "api/Approval/detaildepthead?id=" + $("#id").val(),
        type: "GET",
        cache: false,
        success: function (result) {
            debugger
            var data = result.Data;

            $("#jenispenerimaan").val(data.JENIS_PENERIMAAN);

            //$("#estimasiharga").val(data.ESTIMASI_HARGA);
            var formattedEstimasiHarga = parseFloat(data.ESTIMASI_HARGA).toLocaleString('id-ID', { style: 'currency', currency: 'IDR' });
            $("#estimasiharga").val(formattedEstimasiHarga);

            $("#tempatpenerimaan").val(data.TEMPAT_PENERIMAAN);
            $("#tanggalpenerimaan").val(moment(data.TANGGAL_PENERIMAAN).format("YYYY-MM-DD"));
            $("#alasanpemberian").val(data.ALASAN_PEMBERIAN);
            $("#namapemberi").val(data.NAMA_PEMBERI);
            $("#pekerjaanpemberi").val(data.PEKERJAAN_PEMBERI);
            $("#namaperusahaan").val(data.NAMA_PERUSAHAAN);
            $("#hubunganpemberi").val(data.HUBUNGAN_PEMBERI);
            if (data.BUKTI_GRATIFIKASI) {
                $("#buktiGratifikasiImage").attr("src", data.BUKTI_GRATIFIKASI);
            }
        }
    });
}

function submitApproval(postStatus) {
    let data = new Object();
    data.ID = $("#id").val();
    data.STATUS = postStatus;
    data.UPDATED_BY = userNrp;

    document.getElementById("loadingScreen").style.display = "block";
    $.ajax({
        url: apiUrl + "api/Approval/ApproveDeptHead",
        data: JSON.stringify(data),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            document.getElementById("loadingScreen").style.display = "none";
            if (data.Remarks == true) {
                Swal.fire(
                    'Saved!',
                    'Data has been Saved.',
                    'success'
                ).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = "/Approval/Depthead";
                    }
                });
            } if (data.Remarks == false) {
                Swal.fire(
                    'Error!',
                    'Message : ' + data.Message,
                    'error'
                );
            }
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}

function rejectApproval(postStatus) {
    let data = new Object();
    data.ID = $("#id").val();
    data.STATUS = postStatus;
    data.UPDATED_BY = userNrp;

    document.getElementById("loadingScreen").style.display = "block";
    $.ajax({
        url: apiUrl + "api/Approval/RejectDeptHead",
        data: JSON.stringify(data),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            document.getElementById("loadingScreen").style.display = "none";
            if (data.Remarks == true) {
                Swal.fire(
                    'Saved!',
                    'Data has been Saved.',
                    'success'
                ).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = "/Approval/Depthead";
                    }
                });
            } if (data.Remarks == false) {
                Swal.fire(
                    'Error!',
                    'Message : ' + data.Message,
                    'error'
                );
            }
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}