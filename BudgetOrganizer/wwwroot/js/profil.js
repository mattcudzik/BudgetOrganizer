// Add 'load' event handler
window.addEventListener("load", () => {

    function getIncomeOperationsData() {
        // Preparing operationsRequest to get my data
        const auth = "Bearer " + localStorage.getItem("token");
        const operationsRequest = new Request("https://localhost:7057/api/Operations/me?OnlyPositive=true", {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            }
        });

        fetch(operationsRequest)
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
                const przychodyBody = document.getElementById("przychody_center")
                for (let i = 0; i < json.length; i++) {
                    let obj = json[i];
                    console.log(obj);
                    var newElement = document.createElement("div");
                    newElement.setAttribute("class", "przychod");
                    newElement.setAttribute("style", "border-style:dashed;" +
                        " border-width: 4px;border-color:" + obj.category.color + ';');
                    var colorDiv = document.createElement("div");
                    colorDiv.setAttribute("class", "color");
                    var date = new Date(obj.dateTime);

                    var allDiv = document.createElement("div");
                    allDiv.setAttribute("class", "all");
                    allDiv.innerHTML = '<h3>' + date.toDateString() + '</h3>' + '<p>' + obj.amount + 'dupoaPLN' + '</p>'

                    newElement.appendChild(colorDiv);
                    newElement.appendChild(allDiv);

                    // newElement.setAttribute("style", "background-color:" + obj.color + ';');
                    przychodyBody.appendChild(newElement);
                }
                
            }).catch((error) => {
            console.error(error);
        });
    }

    function getOutcomeOperationsData() {
        // Preparing operationsRequest to get my data
        const auth = "Bearer " + localStorage.getItem("token");
        const operationsRequest = new Request("https://localhost:7057/api/Operations/me?OnlyPositive=false", {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            }
        });

        fetch(operationsRequest)
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
                const przychodyBody = document.getElementById("wydatki_center")
                for (let i = 0; i < json.length; i++) {
                    let obj = json[i];
                    console.log(obj);
                    var newElement = document.createElement("div");
                    newElement.setAttribute("class", "wydatek");
                    newElement.setAttribute("style", "border-style:dashed;" +
                        " border-width: 4px;border-color:" + obj.category.color + ';');
                    var colorDiv = document.createElement("div");
                    colorDiv.setAttribute("class", "color");
                    var date = new Date(obj.dateTime);

                    var allDiv = document.createElement("div");
                    allDiv.setAttribute("class", "all");
                    allDiv.innerHTML = '<h3>' + date.toDateString() + '</h3>' + '<p>' + obj.amount + 'PLN' + '</p>'

                    newElement.appendChild(colorDiv);
                    newElement.appendChild(allDiv);

                    // newElement.setAttribute("style", "background-color:" + obj.color + ';');
                    przychodyBody.appendChild(newElement);
                }

            }).catch((error) => {
            console.error(error);
        });
    }
    function getMyAccountData() {
        // Preparing operationsRequest to get my data
        const auth = "Bearer " + localStorage.getItem("token");
        const operationsRequest = new Request("https://localhost:7057/api/Accounts/me", {
            method: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            }
        });

        fetch(operationsRequest)
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
                const finanse = document.getElementById("finanse")
                finanse.innerHTML = '<p>' + json.budget + '</p>'
                const podsumowanie_p = document.getElementById("podsumowanie_p")
                podsumowanie_p.innerHTML = '<p>' + json.budget + '</p>'
                const podsumowanie_w = document.getElementById("podsumowanie_w")
                podsumowanie_w.innerHTML = '<p>' + json.budget + '</p>'
                
            }).catch((error) => {
            console.error(error);
        });
    }


    function sendData() {

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
    getIncomeOperationsData();
    getOutcomeOperationsData();
    getMyAccountData();

});

