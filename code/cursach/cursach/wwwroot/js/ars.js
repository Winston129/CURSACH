﻿document.addEventListener("DOMContentLoaded", () => {
    document.getElementById('Select_Status').addEventListener("change", () => {
        var Available = document.getElementById("Available");
        var Reserved = document.getElementById("Reserved");
        var Sold = document.getElementById("Sold");

        var Client = document.getElementById("Client");

        var select_status = event.target.value;
        console.log(select_status);

        if (select_status == "Available") {
            Available.style.display = "flex";
            Reserved.style.display = "none";
            Sold.style.display = "none";

            Client.style.display = "none";
        }
        else if (select_status == "Reserved") {
            Available.style.display = "none";
            Reserved.style.display = "flex";
            Sold.style.display = "none";

            Client.style.display = "flex";
        }
        else if (select_status == "Sold") {
            Available.style.display = "none";
            Reserved.style.display = "none";
            Sold.style.display = "flex";

            Client.style.display = "flex";
        }
        else {
            Available.style.display = "none";
            Reserved.style.display = "none";
            Sold.style.display = "none";

            Client.style.display = "none";
        }
    });
});