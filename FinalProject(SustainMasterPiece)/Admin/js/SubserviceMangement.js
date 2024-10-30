async function AddSubServive() {
  event.preventDefault(); // Prevent form from submitting the default way

  const url = `http://localhost:5036/api/SubService/subServices/CreatesubService`;

  const formData = new FormData();
  formData.append("name", document.getElementById("SubserviceName").value);
  formData.append("serviceId", document.getElementById("ServiceId").value);
  formData.append("description", document.getElementById("description").value);

  try {
    const response = await fetch(url, {
      method: "POST", // 'POST' for sending data
      body: formData,
    });

    if (response.ok) {
      alert("SubService added successfully!");
    } else {
      const errorData = await response.json();
      console.error("Error:", errorData);
      alert("Failed to add SubService.");
    }
  } catch (error) {
    console.error("Error:", error);
    alert("Failed to connect to the server.");
  }
}

async function GetSubServices() {
  var url = "http://localhost:5036/api/SubService/subService/GetAllsubServices";
  var response = await fetch(url);
  var result = await response.json();

  var container = document.getElementById("ShowAllSubServiceTable");
  container.innerHTML = ""; // Clear previous data

  result.forEach((subService) => {
    container.innerHTML += `
        <tr>
            <td>${subService.id}</td>
            <td>${subService.name}</td>
            <td>${subService.serviceId}</td>
            <td>${subService.serviceName}</td> <!-- Display Service Name -->
            <td>${subService.description}</td>
            <td>${subService.createdAt}</td>
            <td>
                <button class="btn btn-warning btn-sm" onclick="openEditModal(${subService.id}, '${subService.name}', '${subService.serviceName}', '${subService.description}')">Edit</button>
                <button class="btn btn-danger btn-sm" onclick="deleteSubService(${subService.id})">Delete</button>
            </td>
        </tr>
        `;
  });
}

function openEditModal(id, name, serviceName, description) {
  // Populate the modal fields with sub-service data
  document.getElementById("editSubServiceId").value = id;
  document.getElementById("editSubServiceName").value = name;
  document.getElementById("editSubServiceServiceName").value = serviceName;
  document.getElementById("editSubServiceDescription").value = description;

  // Show the modal
  var editModal = new bootstrap.Modal(
    document.getElementById("editSubServiceModal")
  );
  editModal.show();
}

// Save changes and update sub-service
async function saveSubService() {
  var id = document.getElementById("editSubServiceId").value;
  var name = document.getElementById("editSubServiceName").value;
  var serviceName = document.getElementById("editSubServiceServiceName").value; // Service Name
  var description = document.getElementById("editSubServiceDescription").value;

  // Assuming the API can map ServiceName to ServiceId
  var data = {
    Name: name,
    ServiceName: serviceName, // Send ServiceName instead of ServiceId
    Description: description,
  };

  var url = `http://localhost:5036/api/Service/Services/UpdateService/${id}`;

  // Send PUT request to update sub-service
  let request = await fetch(url, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (request.ok) {
    alert("Sub-service updated successfully.");
    window.location.reload(); // Reload the page to refresh the list
  } else {
    alert("Failed to update sub-service.");
  }
}
// Navigate to update sub-service page
function UpdateSubService(id) {
  localStorage.setItem("subServiceId", id); // Store the subServiceId for editing
  window.location.href = "UpdateSubService.html"; // Redirect to the update page
}

//******************************Delete sub-service by id***************************************
async function deleteSubService(subServiceId) {
  var url = `http://localhost:5036/api/SubService/subServices/DeletesubService/${subServiceId}`;

  let request = await fetch(url, {
    method: "DELETE",
  });

  if (request.ok) {
    alert("The sub-service has been deleted successfully");
    window.location.reload(); // Reload page after deletion
  } else {
    alert("Failed to delete the sub-service");
  }
}

// Call the function to load and display the sub-services
GetSubServices();
