async function GetAllServices() {
  try {
    const selectedService = document.getElementById("serviceFilter").value;
    const url = selectedService
      ? `http://localhost:5036/api/SubService/subService/GetFilteredSubServices?serviceName=${encodeURIComponent(
          selectedService
        )}`
      : "http://localhost:5036/api/SubService/subService/GetAllsubServices";

    const response = await fetch(url);
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }

    const result = await response.json();

    const container = document.getElementById("showAllServices");
    container.innerHTML = ""; // Clear existing content before appending new cards

    result.forEach((service) => {
      container.innerHTML += `
           <div class="col-lg-4 mb-4"> <!-- Use col-lg-4 for 3 cards in a row -->
                <div class="services purple card"> <!-- Use Bootstrap card class for better styling -->
                    <div class="card-body"> <!-- Card body for padding -->
                        <h5 class="card-title">${service.name}</h5>
                        <p class="card-text">${service.description}</p>
                        <a class="btn btn-outline-light" href="ServiceRequest.html" style="color:#33e2a0">Request a Quote</a>
                    </div>
                </div>
            </div>`;
    });
  } catch (error) {
    console.error("There has been a problem with your fetch operation:", error);
  }
}

// Initial call to load all services on page load
GetAllServices();
