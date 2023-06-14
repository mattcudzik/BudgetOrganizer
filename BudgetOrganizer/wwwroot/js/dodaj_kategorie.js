// Add 'load' event handler
window.addEventListener("load", () => {

   
    function sendData() {

        const FD = new FormData(form);
        var bodyPost = '{"name": "' + FD.get("name")
            + '","color": "' + FD.get("color")
            + '"}';

        // console.log(bodyPost);

        const auth = "Bearer " + localStorage.getItem("token");
        // Prepare request
        // TODO API do zmiany na Categoriers/me - po poprawieniu inputu przez Cudzika
        const request = new Request("https://localhost:7057/api/Categories/me", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
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

    const form = document.getElementById("categories-form");


    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
        event.preventDefault();
        sendData();
    });
});

