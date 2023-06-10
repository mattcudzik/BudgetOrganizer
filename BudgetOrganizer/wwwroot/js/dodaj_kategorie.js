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

        const request = new Request("https://localhost:7057/api/Categories", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            },
            body: bodyPost
        });

        // Send request
        try {
            fetch(request)
                .then((response) => {
                    if (!response.ok) {

                        const error = response.status;
                        throw new Error(error)
                    }
                    return response.json()
                })
                .then((json) => {
                    console.log(json);
                })
        } catch (error) {
            console.error(error);
        }

    }

    const form = document.getElementById("categories-form");


    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
        event.preventDefault();
        sendData();
    });
});

