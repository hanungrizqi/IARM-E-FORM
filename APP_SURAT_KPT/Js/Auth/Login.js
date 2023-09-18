$(document).ready(function () {
    $("#login-btn").click(async function () {
        event.preventDefault()
        document.getElementById("loadingScreen").style.display = "block";
        const requestData = {
            Username: $('#nrp').val(),
            Password: $('#password').val()
        }
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            url: apiUrl + "api/login",
            data: JSON.stringify(requestData),
            success: function (response) {
                document.getElementById("loadingScreen").style.display = "none";
                console.log(response)
                if (response.Success == true) {
                    MakeSession(response.Data)
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
    });

    function MakeSession(userData) {
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            url: "/login/LoginUser",
            data: JSON.stringify(userData),
            success: function (response) {
                document.getElementById("loadingScreen").style.display = "none";
                console.log(response)
                if (response.Success == true) {
                    window.location.href = '/dashboard/dashboard'
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
});
