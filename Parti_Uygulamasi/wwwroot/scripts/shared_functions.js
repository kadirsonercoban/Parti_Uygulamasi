function showNewDetails(id) {
    // Ajax isteği yaparak kişi bilgilerini controller'dan al
    $.ajax({
        url: '/MainPage/GetNewsDetails/' + id,
        type: 'GET',
        dataType: 'json', // Dönen verinin tipi, JSON olarak varsayılan olarak ayarlandı
        success: function (data) {
            if (data) {
                // Veri null değilse modal içeriğini oluştur
                var newDetails = `
                        <img src="/images/sample.jpg" alt="${data.head}" class="img-fluid mb-2">
                        <h5>${data.head}</h5>
                        <p>${data.content}</p>
                        <p>${formatDate(data.date)}</p>
        `               ;
                document.getElementById("modal-body").innerHTML = newDetails;
                document.getElementById("exampleModalLabel").textContent = data.head;

                $('#myModal').modal('show');
            } else {
                // Veri null ise burada bir hata mesajı gösterebilirsiniz
                console.log("Haber bilgileri bulunamadı.");
            }
        },
        error: function () {
            console.log("Bir hata oluştu");
        }
    });
}

function formatDate(dateString) {
    const options = { day: 'numeric', month: 'numeric', year: 'numeric' };
    const formattedDate = new Date(dateString).toLocaleDateString(undefined, options);
    return formattedDate;
}


function getPersonInfo(personId, callback) {
    $.ajax({
        url: '/PartyWorkers/GetPartyWorkerDetails/' + personId,
        type: 'GET',
        dataType: 'json', 
        success: function (data) {
            var personModel = {
                Id: data.id,
                FirstName: data.firstName,
                LastName: data.lastName,
                Title: data.title,
                City: data.city,
                Position: data.position,
                Introduction: data.introduction,
                Phone: data.phone,
                Email: data.email,
                WebPage: data.webPage,
                Photo: null
            };
            //console.log(personModel);
            callback(personModel);
        },
        error: function () {
            console.log("Bir hata oluştu");
        }
    });  
}

