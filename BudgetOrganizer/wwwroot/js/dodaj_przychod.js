// Add 'load' event handler
window.addEventListener("load", () => {

    function getMyData() {
        // Preparing request to get my data
        const auth = "Bearer " + localStorage.getItem("token");
        const request = new Request("https://localhost:7057/api/Categories/me", {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            }
        });

        // let out;
        // Send request to get my data and asign my group id
        try {
            fetch(request)
                .then((response) => {
                    if (!response.ok) {
                        // get error message from body and default to response status
                        //     TODO obsługa komunikatu niepoprawnego response

                        const error = response.status;//JSON.stringify(response.json());
                        throw new Error(error);
                    }
                    return response.json()
                })
                .then((json) => {
                    // console.log(json);
                    // out = json;
                    const categoryBody = document.getElementById("category")
                    for (let i = 0; i < json.length; i++) {
                        let obj = json[i];
                        // console.log(obj.id);
                        // console.log(obj.name);
                        // console.log(obj.color);
                        var newElement = document.createElement("option");
                        newElement.setAttribute("value",obj.id);
                        // newElement.setAttribute("style", "background-color:" + obj.color + ';');
                        newElement.innerHTML = obj.name;
                        categoryBody.appendChild(newElement); 
                    }
                });
        }
        catch (error) {
            console.error(error);
        }
    }


    async function sendData() {

        const FD = new FormData(form);
        var bodyPost = '{"categoryId": "' + FD.get("category")
            + '","amount": "' + FD.get("value")
            + '","dateTime": "' + FD.get("start-date")
            + '"}';

        // console.log(bodyPost);

        const auth = "Bearer " + localStorage.getItem("token");
        // Prepare request

        const request = new Request("https://localhost:7057/api/Operations/me", {
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
    

    const form = document.getElementById("login-form");
    getMyData();

    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
        event.preventDefault();
        sendData();
    });
});

