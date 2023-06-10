window.addEventListener("load", ()=>{
    
    function getMyData(){
        const auth = 'Bearer ' + localStorage.getItem('token');
        const request = new Request("https://localhost:7057/api/Categories/me",{
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            }
        });
        
        
        fetch(request)
            .then((response)=>{
                if(!response.ok){
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
                for (let i = 0; i<json.length;i++){
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
    
    function sendData(){
        const FD = new FormData(form)
        var startDate = (FD.get("start-date") != '') ? '","dateTime": "' + FD.get("start-date") : '';

        var bodyPost = '{"categoryId": "' + FD.get("category")
            + '","amount": "' + FD.get("value")
            + startDate
            + '"}';

        const auth = "Bearer " + localStorage.getItem("token");
        
        const request = new Request("https://localhost:7057/api/Operations/me", {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': auth
            },
            body: bodyPost
            });
        
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
    
    const form = document.getElementById("login-form")
    getMyData();
    form.addEventListener("submit", (event)=>{
        event.preventDefault();
        sendData();
    })
})