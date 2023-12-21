$("document").ready(function () {
    document.getElementById("datepickerd").setAttribute("max", new Date().toISOString().split("T")[0]);
})

var radioButtons = document.getElementsByName("optionsRadios");

for (var i = 0; i < radioButtons.length; i++) {
    debugger
    radioButtons[i].addEventListener("change", toggleInput);
}

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

function formatCurrency() {
    var inputValue = document.getElementById('estimasiharga').value;
    var numericValue = inputValue.replace(/\D/g, '');
    var formattedValue = new Intl.NumberFormat('id-ID').format(numericValue);
    document.getElementById('estimasiharga').value = formattedValue;
}

function displayImage() {
    var fileInput = document.getElementById('exampleInputFile');
    var imagePreview = document.getElementById('imagePreview');
    var preview = document.getElementById('preview');

    if (fileInput.files.length > 0) {
        var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
        if (!allowedExtensions.exec(fileInput.value)) {
            alert('Tipe file tidak valid. Harap pilih file dengan tipe jpg, jpeg, atau png.');
            fileInput.value = '';
            imagePreview.style.display = 'none';
        } else {
            imagePreview.style.display = 'block';

            var reader = new FileReader();
            reader.onload = function (e) {
                preview.src = e.target.result;
            };

            reader.readAsDataURL(fileInput.files[0]);
        }
    } else {
        imagePreview.style.display = 'none';
    }
}

function submitGratifikasi() {
    debugger
    $("#submitButton").prop("disabled", true);

    let obj = new Object();
    obj.NRP = userNrp;
    if ($('#fasilitaslainnya').is(':checked')) {
        obj.JENIS_PENERIMAAN = $('#inputLainnya').val();
    } else {
        obj.JENIS_PENERIMAAN = $('input[name=optionsRadios]:checked').val();
    }
    //obj.ESTIMASI_HARGA = $('#estimasiharga').val();
    obj.ESTIMASI_HARGA = $('#estimasiharga').val().replace(/\./g, '');
    obj.TEMPAT_PENERIMAAN = $('#tempatpenerimaan').val();
    obj.TANGGAL_PENERIMAAN = $('#datepickerd').val();
    obj.NAMA_PEMBERI = $('#namapemberi').val();
    obj.PEKERJAAN_PEMBERI = $('#pekerjaanpemberi').val();
    obj.NAMA_PERUSAHAAN = $('#namaperusahaan').val();
    obj.HUBUNGAN_PEMBERI = $('#hubungandgpemberi').val();
    obj.ALASAN_PEMBERIAN = $('#alasanpemberian').val();

    if (obj.ESTIMASI_HARGA == ""
        || obj.TEMPAT_PENERIMAAN == ""
        || obj.TANGGAL_PENERIMAAN == ""
        || obj.NAMA_PEMBERI == ""
        || obj.PEKERJAAN_PEMBERI == ""
        || obj.NAMA_PERUSAHAAN == ""
        || obj.HUBUNGAN_PEMBERI == ""
        || obj.ALASAN_PEMBERIAN == ""
        || obj.JENIS_PENERIMAAN == "" || obj.JENIS_PENERIMAAN == undefined
        || $("#exampleInputFile").val() == ""
    ) {
        Swal.fire(
            'Warning',
            'Please complete all input data.',
            'warning'
        );
        $("#submitButton").prop("disabled", false);
        return;
    }
    if (!$('#cekboks').is(':checked')) {
        Swal.fire({
            title: 'Warning!',
            text: 'Please confirm that you have filled in the gratification report truthfully.',
            icon: 'warning',
            confirmButtonText: 'OK'
        });
        $("#submitButton").prop("disabled", false);
        return;
    }

    document.getElementById("loadingScreen").style.display = "block";
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
                $("#submitButton").prop("disabled", false);
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
            document.getElementById("loadingScreen").style.display = "none";
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
                $("#submitButton").prop("disabled", false);
            } if (data.Remarks == false) {
                Swal.fire(
                    'Error!',
                    'Message : ' + data.Message,
                    'error'
                );
                $("#submitButton").prop("disabled", false);
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
    $('#datepickerd').val('');
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
    $("#imagePreview").hide();
    $("#preview").attr("src", "");
}
