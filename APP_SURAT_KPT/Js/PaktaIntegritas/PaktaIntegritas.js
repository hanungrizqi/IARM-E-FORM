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

    $("#btn-submit-pi").click(async function () {
        event.preventDefault()
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