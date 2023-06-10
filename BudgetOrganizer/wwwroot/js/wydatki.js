window.addEventListener("load", () => {

    function getMyData() {
        // Preparing request to get my data
        const FD = new FormData(form)

        const auth = "Bearer " + localStorage.getItem("token");

        const getUrl = new URL("https://localhost:7057/api/Operations/me");
        getUrl.searchParams.set('OnlyPositive', 'false');
        getUrl.searchParams.set('sortOrder', FD.get("sort"));


        //console.log(FD.get("sort"))
        // Setting filtering and sorting params
        if (FD.get("data_od") != '') getUrl.searchParams.set('DateFrom', FD.get("data_od"));
        if (FD.get("data_do") != '') getUrl.searchParams.set('DateTo', FD.get("data_do"));
        if (FD.get("kwota_od") != '') getUrl.searchParams.set('AmountFrom', FD.get("kwota_od"));
        if (FD.get("kwota_do") != '') getUrl.searchParams.set('AmountTo', FD.get("kwota_do"));
        if (FD.get("category") != "null") getUrl.searchParams.set('CategoriesId', FD.get("category"));


        const request = new Request(getUrl, {
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
                        throw new Error(error)
                    })
                }
                return response.json()
            })
            .then((json) => {
                const operationDiv = document.getElementById("scroll");
                operationDiv.innerHTML = "";
                for (let i = 0; i < json.length; i++) {
                    let obj = json[i];
                    console.log(obj);
                    var newElement = document.createElement("div");
                    newElement.setAttribute("class", "wydatek1");
                    newElement.setAttribute("style", "border-style:dashed; border-width: 4px;border-color:" + obj.category.color + ';');

                    var colorDiv = document.createElement("div");
                    colorDiv.setAttribute("class", "color");


                    var date = new Date(obj.dateTime);

                    var allDiv = document.createElement("div");
                    allDiv.setAttribute("class", "all");
                    allDiv.innerHTML = '<h3>' + date.toDateString() + '</h3>' + '<p>' + obj.amount + '</p>'

                    newElement.appendChild(colorDiv);
                    newElement.appendChild(allDiv);

                    operationDiv.appendChild(newElement);
                }
            }).catch((error) => {
                console.error(error);
            });
    }

    function getMyDataCategory() {
        const auth = 'Bearer ' + localStorage.getItem('token');
        const request = new Request("https://localhost:7057/api/Categories/me", {
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
                const categoryBody = document.getElementById("category")
                for (let i = 0; i < json.length; i++) {
                    let obj = json[i];

                    var newElement = document.createElement("option");
                    newElement.setAttribute("value", obj.id)
                    newElement.innerHTML = obj.name;
                    categoryBody.appendChild(newElement);
                }
            }).catch((error) => {
                console.error(error);
            });
    }



    const form = document.getElementById("right1");
    getMyDataCategory();
    getMyData();

    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
        event.preventDefault();
        getMyData();
    });
});