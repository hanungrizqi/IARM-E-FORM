$(document).ready(function () {
    function initUserData() {
        document.getElementById("loadingScreen").style.display = "block";
        var request = $.ajax({
            url: `${apiUrl}api/integritas/getdataintegritas/${userNrp}`,
            method: "GET",
        }).done(function (response) {
            if (response.Status == true) {
                if (response.Data.IsSubmitted == true) {
                    document.getElementById("form-pi").style.display = "none";
                    document.getElementById("form-pi-submit").style.display = "block";
                    document.getElementById("loadingScreen").style.display = "none";
                } else {
                    document.getElementById("form-pi").style.display = "block";
                    document.getElementById("form-pi-submit").style.display = "none";

                    document.getElementById("form-name").innerHTML = response.Data.Name;
                    document.getElementById("sign-name").innerHTML = response.Data.Name;
                    document.getElementById("form-nrp").innerHTML = response.Data.Nrp;
                    document.getElementById("form-dept").innerHTML = response.Data.Dept;


                    document.getElementById("loadingScreen").style.display = "none";
                }
            } else {
                document.getElementById("form-pi").style.display = "none";
                document.getElementById("form-pi-submit").style.display = "block";
                document.getElementById("loadingScreen").style.display = "none";
            }
            console.log(response.Data)
            
        }).fail(function (jqXHR, textStatus) {
            console.log(jqXHR, textStatus);
        });
    }

    // Function to check if all checkboxes are checked
    function areAllCheckboxesChecked() {
        debugger
        var checkboxes = $('.user-checkbox');
        return checkboxes.length === checkboxes.filter(':checked').length;
    }

    // Function to enable or disable the submit button based on checkbox status
    function updateSubmitButtonStatus() {
        debugger
        var btnSubmit = $('#btn-submit-pi');
        btnSubmit.prop('disabled', !areAllCheckboxesChecked());
    }

    // Attach a change event listener to all checkboxes
    $('.user-checkbox').change(function () {
        debugger
        updateSubmitButtonStatus();
    });

    // Call the function initially to set the initial state of the submit button
    updateSubmitButtonStatus();

    $("#btn-submit-pi").click(async function () {
        event.preventDefault()

        // Menyimpan status checkbox dalam array
        //debugger
        //var checkboxes1 = $('#pi-1');
        //var checkboxes2 = $('#pi-2');
        //var checkboxes3 = $('#pi-3');
        //var checkboxes4 = $('#pi-4');
        //var checkboxes5 = $('#pi-5');
        //var checkboxes6 = $('#pi-6');
        //var checkboxes7 = $('#pi-7');
        //var isChecked1 = Array.from(checkboxes1).some(checkbox => checkbox.checked);
        //var isChecked2 = Array.from(checkboxes2).some(checkbox => checkbox.checked);
        //var isChecked3 = Array.from(checkboxes3).some(checkbox => checkbox.checked);
        //var isChecked4 = Array.from(checkboxes4).some(checkbox => checkbox.checked);
        //var isChecked5 = Array.from(checkboxes5).some(checkbox => checkbox.checked);
        //var isChecked6 = Array.from(checkboxes6).some(checkbox => checkbox.checked);
        //var isChecked7 = Array.from(checkboxes7).some(checkbox => checkbox.checked);

        var signLocation = $('#sign-location').val();

        const requestData = {
            UserNrp: userNrp,
            Location: signLocation
        }

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
        //else if (!isChecked1 || !isChecked2 || !isChecked3 || !isChecked4 || !isChecked5 || !isChecked6 || !isChecked7) {
        //    debugger
        //    Swal.fire({
        //        icon: 'error',
        //        title: "Anda harus menyetujui semua syarat dan ketentuan!",
        //        confirmButtonText: 'Ok'
        //    }).then((result) => {
        //        if (result.isConfirmed) {
        //            document.getElementById("loadingScreen").style.display = "none";
        //        }
        //    })
        //}
        else {
            console.log(requestData)
            Swal.fire({
                icon: 'warning',
                title: "Apakah anda menyetujui Pakta Integritas ini",
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
                        url: apiUrl + "api/integritas/submit",
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
                                //generatePDF();
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
    });

    initUserData();
})

function generatePDF() {
    debugger
    $.ajax({
        url: `${apiUrl}api/integritas/pdffile?nrp=${userNrp}`,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
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
            } else {
                Swal.fire(
                    'Error!',
                    'Message: ' + data.Message,
                    'error'
                );
            }
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}