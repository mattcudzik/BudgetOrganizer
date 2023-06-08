// Add 'load' event handler
window.addEventListener("load", () => {

   
    async function sendData() {

        const FD = new FormData(form);
        var bodyPost = '{"name": "' + FD.get("name")
            + '","color": "' + FD.get("color").substring(1,7)
            + '"}';

        console.log(bodyPost);

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

            const response = await fetch(request);
            const json = await response.json();
            if (!response.ok) {
                // get error message from body and default to response status

                const error = JSON.stringify(json);
                throw new Error(error);
            }
            console.log(json);
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

