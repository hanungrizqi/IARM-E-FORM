
function toggleInput() {
    debugger
    var lainnyaInput = document.getElementById("lainnyaInput");
    var inputLainnya = document.getElementById("inputLainnya");
    var fasilitasLainnyaRadio = document.getElementById("fasilitaslainnya");

    if (fasilitasLainnyaRadio.checked) {
        lainnyaInput.style.display = "block";
    } else {
        lainnyaInput.style.display = "none";
    }
}

$('#datepicker').datepicker({
    autoclose: true
})

function submitGratifikasi() {
    debugger

    if (!$('#cekboks').is(':checked')) {
        Swal.fire({
            title: 'Warning!',
            text: 'Please confirm that you have filled in the gratification report truthfully.',
            icon: 'warning',
            confirmButtonText: 'OK'
        });
        return;
    }

    let obj = new Object();
    obj.NRP = userNrp;
    if ($('#fasilitaslainnya').is(':checked')) {
        obj.JENIS_PENERIMAAN = $('#inputLainnya').val();
    } else {
        obj.JENIS_PENERIMAAN = $('input[name=optionsRadios]:checked').val();
    }
    obj.ESTIMASI_HARGA = $('#estimasiharga').val();
    obj.TEMPAT_PENERIMAAN = $('#tempatpenerimaan').val();
    obj.TANGGAL_PENERIMAAN = $('#datepicker').val();
    obj.NAMA_PEMBERI = $('#namapemberi').val();
    obj.PEKERJAAN_PEMBERI = $('#pekerjaanpemberi').val();
    obj.NAMA_PERUSAHAAN = $('#namaperusahaan').val();
    obj.HUBUNGAN_PEMBERI = $('#hubungandgpemberi').val();
    obj.ALASAN_PEMBERIAN = $('#alasanpemberian').val();

    if ($("#estimasiharga").val() == ""
        || $("#tempatpenerimaan").val() == ""
        || $("#datepicker").val() == ""
        || $("#namapemberi").val() == ""
        || $("#pekerjaanpemberi").val() == ""
        || $("#namaperusahaan").val() == ""
        || $("#hubungandgpemberi").val() == ""
        || $("#alasanpemberian").val() == ""
        || $("#tempatpenerimaan").val() == ""
        || $("#fasilitaslainnya").val() == ""
        || $("#exampleInputFile").val() == ""
    ) {
        Swal.fire(
            'Warning',
            'Mohon lenkapi semua data inputan.',
            'warning'
        );
        return;
    }

    $.ajax({
        url: apiUrl + "api/Gratifikasi/submitGratifikasi",
        data: JSON.stringify(obj),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.Remarks == true) {
                buktiGratifikasi();
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
    })
}

function buktiGratifikasi() {
    debugger
    var fileInput = document.getElementById('exampleInputFile');
    var file = fileInput.files[0];
    var formData = new FormData();
    formData.append('file', file);

    console.log(file);

    $.ajax({
        url: apiUrl + 'api/Gratifikasi/uploadBuktiGratifikasi',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.Remarks == true) {
                Swal.fire(
                    'Saved!',
                    'Data has been Saved.',
                    'success'
                );
                resetForm();
                $('#modal-insert').modal('hide');
                $('.select2-modal').val('').trigger('change');
                $('.form-control').val('');
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

function resetForm() {
    $('input[name=optionsRadios]').prop('checked', false);
    $('#estimasiharga').val('');
    $('#tempatpenerimaan').val('');
    $('#datepicker').val('');
    $('#namapemberi').val('');
    $('#pekerjaanpemberi').val('');
    $('#namaperusahaan').val('');
    $('#hubungandgpemberi').val('');
    $('#alasanpemberian').val('');
    $('#exampleInputFile').val('');

    $('#cekboks').prop('checked', false);
    if ($('#lainnyaInput').is(':visible')) {
        toggleInput();
    }
}