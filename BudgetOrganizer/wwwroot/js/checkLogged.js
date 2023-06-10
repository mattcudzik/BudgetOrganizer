window.addEventListener("load", () => {

    function check() {
        // Preparing request to get my data
       

        const auth = "Bearer " + localStorage.getItem("token");

        const request = new Request("https://localhost:7057/api/Accounts/me", {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth,
            },

        });

        // let out;
        // Send request to get my data and asign my group id

        fetch(request)
            .then((response) => {
                if (!response.ok) {
                    // get error message from body and default to response status
                    //     TODO obsługa komunikatu niepoprawnego response
                    return response.text().then((text) => {
                        const error = response.status + ' ' + text;
                        if (response.status = '401') window.location.replace('login.html');
                        throw new Error(error)
                    })
                }
                return response.json()
            }).catch((error) => {
                console.error(error);
            });
    }

    check();

});