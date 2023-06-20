// Add 'load' event handler
window.addEventListener("load", () => {

    function sendData() {

        const FD = new FormData(form);
        var bodyPost = '{"userName": "' + FD.get("username")
            + '","password": "' + FD.get("password")
            + '","email": "' + FD.get("email")
            + '","budget": "' + FD.get("quantity")
            + '"}';

        // Prepare request
        const request = new Request("https://localhost:7057/api/Accounts/Register", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: bodyPost
        });
        errorP.innerHTML = "";
        // Send request

        fetch(request)
            .then((response) => {
                if (!response.ok) {

                    return response.json().then((text) => {
                        let error = "";
                        for (let i = 0; i < text.length; i++) {
                            error += text[i].description + '</br>'
                        }
                        throw new Error(error)
                    })
                }
                return response.json()
            })
            .then((json) => {
                console.log(json);
                window.location.replace('login.html');
            }).catch((error) => {
                
               //alert(error.message);
                let text = error.message;
                let result = text.fontcolor("red")
                errorP.innerHTML = result;
            });

    }

    const form = document.getElementById("login-form");
    const errorP = document.getElementById("error");
    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
        event.preventDefault();
        sendData();
    });
});

