﻿// Add 'load' event handler
window.addEventListener("load", () => {

    function sendData() {

    const FD = new FormData(form);
    var bodyPost = '{"userName": "' + FD.get("username") + '","password": "' + FD.get("password") + '"}';

    // Prepare request
    const request = new Request("https://localhost:7057/api/Accounts/Login", {
    method: "POST",
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    },
    body: bodyPost
    });


    // Send request

        fetch(request)
            .then((response) => {
                if (!response.ok) {

                    return response.text().then((text) => {
                        const error = response.status + ' ' + text;
                        throw new Error(error)
                    })
                }
                return response.json()
            })
            .then((json) => {
                // Store JWT in localStorage
                localStorage.setItem("token", json.replace(/["]/g, ''));
                console.log(json);
                location.href = 'profil.html'
            }).catch((error) => {
                console.error(error);
            }
       );
    




}

    const form = document.getElementById("login-form");

    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
    event.preventDefault();
    sendData();
    });
});

