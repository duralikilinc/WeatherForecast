function ForgetPassword(mailAdress) {
    var mail = mailAdress;
    $.ajax({
        type: "POST",
        url: 'Account/ForgetPassword',
        contentType: 'application/json',
        data: JSON.stringify({ email: mail }),
        dataType: "json",
        success: function (veri) {
            if (veri.Success) {
                alert(veri.Message);
            } else {
                alert(veri.Message);
            }

        },
        error: function () {

        },
        beforeSend: function () {

        },
        complete: function () {

        }
    });
}

function NewPasswordCreate(id, pas1, pas2) {

    var password = pas1;
    if (pas1 != pas2) {
        alert("Şifreler uyuşmamaktadır.");
    } else {
        $.ajax({
            type: "POST",
            url: 'NewCreatePass',
            contentType: 'application/json',
            data: JSON.stringify({ id: id, pasword: password }),
            dataType: "json",
            success: function (veri) {
                if (veri) {
                    window.location.href = '../Account/Login';
                    alert("Şifre Yenileme işlemi başarılı olmuştur.");
                } else {
                    window.location.href = '../Account/Login';
                    alert("Şifreniz yenilenememiştir.");
                }

            },
            error: function () {
                alert("Bir hata ile meydana gelmiştir. Lütfen sonra tekrar deneyiniz.");
            },
            beforeSend: function () {

            },
            complete: function () {

            }
        });
    }


}

// Lokasyon bilgisini al.
function getLocation() {

    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(showPosition);
    } else {
        alert("Tarayıcınız Geolocation desteklemiyor.");
    }
}

function showPosition(position) {

    var Latitude = position.coords.latitude;
    var Longitude = position.coords.longitude;
    var result = getLocationWeather(Latitude, Longitude);
    return result;
}
function WeatherApi(city) {
    var url = "http://api.openweathermap.org/data/2.5/weather?q=";
    var cityName = city.toUpperCase();
    var apikey = "cf539a523e1287c1062aec275553f896";
    var apiurl = url + cityName + "&units=metric&appid=" + apikey + "&lang=tr";

    $.getJSON(apiurl, function (data) {

        $("#loadTable").find("tr:gt(0)").remove();

        var description = data.weather[0].description.toUpperCase();
        var satir = $("<tr>");
        var hucre1 = $("<td>").text(data.name);
        var hucre2 = $("<td>").text(description);
        var hucre3 = $("<td>").text(data.main.temp);
        var hucre4 = $("<td>").text(data.main.feels_like);
        var hucre5 = $("<td>").text(data.main.temp_min);
        var hucre6 = $("<td>").text(data.main.temp_max);
        var hucre7 = $("<td>").text(data.main.humidity);


        satir.append(hucre1);
        satir.append(hucre2);
        satir.append(hucre3);
        satir.append(hucre4);
        satir.append(hucre5);
        satir.append(hucre6);
        satir.append(hucre7);

        $("#loadTable").append(satir);


    })
        .fail(function () {
            alert("Veri getirilirken bir hata meydana geldi.");
        });
}

function getLocationWeather(lat, lon) {
    var url = "http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon;
    var apikey = "cf539a523e1287c1062aec275553f896";
    var apiUrl = url + "&units=metric&appid=" + apikey + "&lang=tr";
    var mains = [];

    $.getJSON(apiUrl, function (data) {
        mains.push({
            "name": data.name,
            "description": data.weather[0].description,
            "temp": data.main.temp
        });

        var location = mains[0].name;
        var description = mains[0].description;
        var temp = mains[0].temp + " (°C)";

        document.getElementById('location').innerText = location.toUpperCase();
        document.getElementById('description').innerText = description.toUpperCase();
        document.getElementById('temp').innerText = temp;

    });
}
