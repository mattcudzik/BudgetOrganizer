﻿// Add 'load' event handler
window.addEventListener("load", () => {

    function getMyData() {
        // Preparing request to get my data
        const auth = "Bearer " + localStorage.getItem("token");
        const request = new Request("https://localhost:7057/api/Accounts/me", {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            }
        });

        // Send request to get my data and asign my group id
        fetch(request)
            .then((response) => {
                if (!response.ok) {
                    // get error message from body and default to response status
                    return response.text().then((text) => {
                        const error = response.status + ' ' + text;
                        throw new Error(error)
                    })
                }
                return response.json()
            })
            .then((json) => {
                // console.log(json);
                myGroupID = json["groupId"];
                // console.log(myGroupID);
            }).catch((error) => {
            console.error(error);
        });
    }
    
    function sendData() {
        
        const FD = new FormData(form);
        var bodyPost = '{"userName": "' + FD.get("username")
            + '","password": "' + FD.get("password")
            + '","email": "' + FD.get("email")
            + '","budget": "' + FD.get("quantity")
            + '","groupId": "' + myGroupID
            + '"}';

        // console.log(bodyPost);

        // Prepare request
        const request = new Request("https://localhost:7057/api/Accounts/Register", {
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
                console.log(json);
            }).catch((error) => {
            console.error(error);
        });

    }

    const form = document.getElementById("login-form");
    var myGroupID = null;
    getMyData();

    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
        event.preventDefault();
        sendData();
    });
});

