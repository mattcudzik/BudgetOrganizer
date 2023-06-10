window.addEventListener("load", () => {

    function getMyData() {
        // Preparing request to get my data
        const auth = "Bearer " + localStorage.getItem("token");

        const getUrl = new URL("https://localhost:7057/api/Operations/me"); 
        getUrl.searchParams.set('OnlyPositive', 'true');
        //console.log(getUrl)
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
                const categoryBody = document.getElementById("category")
                for (let i = 0; i < json.length; i++) {
                    let obj = json[i];
                    console.log(obj);
                    //var newElement = document.createElement("option");
                    //newElement.setAttribute("value", obj.id);
                    //newElement.innerHTML = obj.name;
                    //categoryBody.appendChild(newElement);
                }
            }).catch((error) => {
                console.error(error);
            });
    }
    

   
    

    const form = document.getElementById("right1");
    getMyData();

    // Add 'submit' event handler
    form.addEventListener("submit", (event) => {
        event.preventDefault();
        getMyData();
    });
});