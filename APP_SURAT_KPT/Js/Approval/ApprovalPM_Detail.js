$("document").ready(function () {
    getDetail();
})

function getDetail() {
    $.ajax({
        url: apiUrl + "api/Approval/detailpm?id=" + $("#id").val(),
        type: "GET",
        cache: false,
        success: function (result) {
            var data = result.Data;
            $("#jenispenerimaan").val(data.JENIS_PENERIMAAN);
            $("#estimasiharga").val(data.ESTIMASI_HARGA);
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

    $.ajax({
        url: apiUrl + "api/Approval/ApprovePM",
        data: JSON.stringify(data),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Remarks == true) {
                Swal.fire(
                    'Saved!',
                    'Data has been Saved.',
                    'success'
                ).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = "/Approval/PM";
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

    $.ajax({
        url: apiUrl + "api/Approval/RejectPM",
        data: JSON.stringify(data),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Remarks == true) {
                Swal.fire(
                    'Saved!',
                    'Data has been Saved.',
                    'success'
                ).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = "/Approval/PM";
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