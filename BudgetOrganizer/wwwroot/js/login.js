// Add 'load' event handler
window.addEventListener("load", () => {

    async function sendData() {

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
    try {

        const response = await fetch(request);
        const json = await response.json();
        if (!response.ok) {
            // get error message from body and default to response status
            const error = response.status+': '+ json;
            throw new Error(error);
        }


        //console.log(json.replace(/["]/g, ''));
        // Store JWT in localStorage
        localStorage.setItem("token", json.replace(/["]/g, ''));

    } catch (error) {
        console.error(error);
    }

}

    const form = document.getElementById("login-form");

    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
    event.preventDefault();
    sendData();
    });
});

