window.addEventListener("load", () => {

    function getMyData() {
        const auth = 'Bearer ' + localStorage.getItem('token');
        const request = new Request("https://localhost:7057/api/Groups/me", {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            }
        });


        fetch(request)
            .then((response) => {
                if (!response.ok) {
                    //     TODO obsługa komunikatu niepoprawnego response
                    return response.text().then((text) => {
                        const error = response.status + ' ' + text;
                        throw new Error(error)
                    })
                }
                return response.json()
            })
            .then((json) => {
                const personBody = document.getElementById("person")

                for (let i = 1; i < json.accounts.length; i++) {
                    let obj = json.accounts[i];
                    console.log(obj);
                    var newElement = document.createElement("option");
                    newElement.setAttribute("value", obj.id)
                    newElement.innerHTML = obj.userName;
                    personBody.appendChild(newElement);
                }
            }).catch((error) => {
                console.error(error);
            });
    }

    function sendData() {
        const FD = new FormData(form)

        var bodyPost = '{"amount": "' + FD.get("amount")
            + '","destinationAccount": "' + FD.get("person")
            + '"}';

        const auth = "Bearer " + localStorage.getItem("token");

        const request = new Request("https://localhost:7057/api/Operations/me/transfer", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            },
            body: bodyPost
        });
        console.log(bodyPost)
        fetch(request)
            .then((response) => {
                if (!response.ok) {

                    return response.text().then((text) => {
                        const error = response.status + ' ' + text;
                        throw new Error(error)
                    })
                }
            })
            .catch((error) => {
                console.error(error);
            });
    }

    const form = document.getElementById("transfer-form")
    getMyData();
    form.addEventListener("submit", (event) => {
        event.preventDefault();
        sendData();
    })
})