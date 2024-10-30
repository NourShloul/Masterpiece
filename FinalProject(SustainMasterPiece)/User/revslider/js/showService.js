var url = "http://localhost:5036/api/Service/Service/GetAllServices";
async function GetAllServices() {
  var response = await fetch(url);

  var result = await response.json();

  var container = document.getElementById("showAllServices");
  result.forEach((service) => {
    container.innerHTML += `   
    <div class="col-lg-6 mb-4">
              <div class="services purple">
                <div class="services-info">
                  <div class="service-shap purple">
                    <i class="ion-ios-cloud-download-outline"></i>
                  </div>
                  <h5 class="float-left mt-4">${service.name}</h5>
                  <div class="clearfix"></div>
                  <p class="mt-3 mb-0">
                    ${service.description}
                  </p>
                </div>
           <a class="slide-button" onclick="toggleSubServices(${service.id}, this)" style="color:#6f42c1">Show Subservices</a>
          <!-- Subservices accordion -->
          <div id="subServices-${service.id}" class="subservices-list mt-3" style="display:none;">
            <ul class="list-group list-group-flush"></ul> <!-- Dynamically populated -->
          </div>
      </div>
    </div>`;
  });
}
GetAllServices();
function storeServiceId(serviceId) {
  localStorage.setItem("serviceId", serviceId);
  window.location.href = "service.html";
}
async function toggleSubServices(serviceId, button) {
  const subServicesContainer = document.getElementById(
    `subServices-${serviceId}`
  );

  if (subServicesContainer.style.display === "none") {
    // Fetch subservices if they haven't been loaded yet
    if (!subServicesContainer.getAttribute("data-loaded")) {
      try {
        const response = await fetch(
          `http://localhost:5036/api/SubService/subServices/GetSubServicesByServiceId/${serviceId}`
        );
        if (response.ok) {
          const subServices = await response.json();

          const subServicesList = subServicesContainer.querySelector("ul");
          subServicesList.innerHTML = ""; // Clear previous list

          subServices.forEach((subService) => {
            const listItem = document.createElement("li");

            // Adding an icon and text for each subservice
            listItem.innerHTML = `<i class="fas fa-check-circle"></i> ${subService.name}`; // Assuming subservice has a name property
            subServicesList.appendChild(listItem);
          });

          subServicesContainer.setAttribute("data-loaded", "true"); // Mark as loaded
        } else {
          console.log("No subservices found for the selected service.");
        }
      } catch (error) {
        console.error("Error fetching subservices:", error);
      }
    }

    // Show the subservices
    subServicesContainer.style.display = "block";
    button.textContent = "Hide Subservices";
  } else {
    // Hide the subservices
    subServicesContainer.style.display = "none";
    button.textContent = "Show Subservices";
  }
}
