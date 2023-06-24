// Add 'load' event handler
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

                    return response.json().then((text) => {
                        throw new Error(text)
                    })
                }
                return response.json()
            })
            .then((json) => {
                // Store JWT in localStorage
                localStorage.setItem("token", json.replace(/["]/g, ''));
                console.log(json);
                location.href = 'profil.html';
            }).catch((error) => {
                let text = error.message;
                let result = text.fontcolor("red")
                errorP.innerHTML = result;
                console.error(error);
            }
       );
    




}

    const form = document.getElementById("login-form");
    const errorP = document.getElementById("error");
    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
    event.preventDefault();
    sendData();
    });
});

