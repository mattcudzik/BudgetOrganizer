window.addEventListener("load", () => {

    const water = document.getElementById("negativeYeast")
    const water2 = document.getElementById("positiveYeast")


    new Chart(water, {
        type: 'doughnut',
        data: {
            labels: [
                'Red',
                'Blue',
                'Yellow'
            ],
            datasets: [{
                label: 'wypływ',
                data: [300, 50, 100],
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                    'rgb(255, 205, 86)'
                ],
                borderWidth: 0.5
            }]
        },
        options: {
            plugins: {
                title: {
                    display: true,
                    text: 'Wypływy'
                },
                legend: {
                    display: false,
                }
            }
        }
    });

    new Chart(water2, {
        type: 'doughnut',
        data: {
            labels: [
                'Red',
                'Blue',
                'Yellow'
            ],
            datasets: [{
                label: 'wpływ',
                data: [300, 50, 100],
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                    'rgb(255, 205, 86)'
                ],
                borderWidth: 0.5
            }]
        },
        options: {
            plugins: {
                title: {
                    display: true,
                    text: 'Wypływy'
                },
                legend: {
                    display: false,
                }
            }
        }
    });



});