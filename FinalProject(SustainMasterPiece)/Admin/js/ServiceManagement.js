//*******************Add Service**********************************
async function AddServec() {
  event.preventDefault(); // Prevent form from submitting the default way

  const url = `http://localhost:5036/api/Service/Services/CreateService`;

  const formData = new FormData();
  formData.append("name", document.getElementById("ServiceName").value);
  formData.append("description", document.getElementById("Description").value);

  try {
    const response = await fetch(url, {
      method: "POST", // 'POST' for sending data
      body: formData,
    });

    if (response.ok) {
      alert("Service added successfully!");
    } else {
      const errorData = await response.json();
      console.error("Error:", errorData);
      alert("Failed to add Service.");
    }
  } catch (error) {
    console.error("Error:", error);
    alert("Failed to connect to the server.");
  }
}

//************************************Show All Service (Get)*******************************************
async function getData() {
  const url = `http://localhost:5036/api/Service/Service/GetAllServices`;

  try {
    const response = await fetch(url);
    let data = await response.json();
    console.log(data);
    let tableBody = document.getElementById("Information"); // Correctly refer to the table body element

    data.forEach((service) => {
      tableBody.innerHTML += `
    <!-- New Service Card (Digital Marketing) -->
    <div class="col-xl-4 col-md-6 mb-4">
      <div class="service-card" data-id="${service.id}">
        <h5>${service.name}</h5>
        <div class="service-info">
          <p><strong>Description:</strong> ${service.description}</p>
        </div>
        <div class="service-actions">
          <a href="#" class="edit" data-id="${service.id}" data-name="${service.name}" data-description="${service.description}">
            <i class="fas fa-edit"></i> Edit
          </a>
          <a href="#" class="delete" data-id="${service.id}">
            <i class="fas fa-trash"></i> Delete
          </a>
        </div>
      </div>
    </div>
  `;
    });

    // Add event listeners for Edit and Delete buttons
    document.querySelectorAll(".edit").forEach((editBtn) => {
      editBtn.addEventListener("click", function (event) {
        const serviceId = this.getAttribute("data-id");
        const serviceName = this.getAttribute("data-name");
        const serviceDescription = this.getAttribute("data-description");

        // Populate modal with current service data
        document.getElementById("serviceName").value = serviceName;
        document.getElementById("serviceDescription").value =
          serviceDescription;

        // Show modal
        const editModal = new bootstrap.Modal(
          document.getElementById("editServiceModal")
        );
        editModal.show();

        // Save changes when the save button is clicked
        document.getElementById("saveEditButton").onclick = async function () {
          const updatedName = document.getElementById("serviceName").value;
          const updatedDescription =
            document.getElementById("serviceDescription").value;

          await editService(serviceId, updatedName, updatedDescription);
          editModal.hide();
        };
      });
    });

    document.querySelectorAll(".delete").forEach((deleteBtn) => {
      deleteBtn.addEventListener("click", function (event) {
        const serviceId = this.getAttribute("data-id");
        if (confirm("Are you sure you want to delete this service?")) {
          deleteService(serviceId);
        }
      });
    });
  } catch (error) {
    console.error("Error fetching data:", error);
  }
}

getData();

// Function to store serviceId when "Edit" button is clicked and populate the modal form
function UpdateService(id) {
  localStorage.setItem("serviceId", id); // Store service ID in localStorage
  populateServiceForm(); // Populate form with existing service data
}

// Function to populate the modal form with the current service data
async function populateServiceForm() {
  const serviceId = localStorage.getItem("serviceId"); // Get service ID from localStorage
  const url = `http://localhost:5036/api/Service/Services/GetServiceById/${serviceId}`;

  try {
    const response = await fetch(url);
    const service = await response.json();

    // Populate the form fields with the service data
    document.getElementById("serviceName").value = service.name;
    document.getElementById("serviceDescription").value = service.description;
  } catch (error) {
    console.error("Error fetching service data:", error);
  }
}

//****************************Edit Service*******************************
// Function to update the service when "Save changes" is clicked
async function UpdateServiceform() {
  event.preventDefault(); // Prevent form submission

  const serviceId = localStorage.getItem("serviceId"); // Get the service ID from localStorage
  const url = `http://localhost:5036/api/Service/Services/UpdateService/${serviceId}`;

  // Get the updated values from the modal form
  const updatedName = document.getElementById("serviceName").value;
  const updatedDescription =
    document.getElementById("serviceDescription").value;

  const requestBody = {
    name: updatedName,
    description: updatedDescription,
  };

  try {
    let response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(requestBody), // Send the updated data as JSON
    });

    if (response.ok) {
      alert("Service updated successfully");
      window.location.href = "servicesadmin.html"; // Redirect to admin page after success
    } else {
      alert("Failed to update service");
    }
  } catch (error) {
    console.error("Error updating service:", error);
  }
}
//******************************Delete Service ****************************
// Function to call the delete API
async function deleteService(id) {
  const url = `http://localhost:5036/api/Service/Services/DeleteService/${id}`;

  try {
    const response = await fetch(url, {
      method: "DELETE",
    });

    if (response.ok) {
      alert("Service deleted successfully");
      location.reload(); // Reload the page to remove the deleted service
    } else {
      alert("Failed to delete service");
    }
  } catch (error) {
    console.error("Error deleting service:", error);
  }
}
