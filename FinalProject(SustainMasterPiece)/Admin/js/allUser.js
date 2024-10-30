async function getData() {
  var url = "http://localhost:5036/api/User/getAllUsers";

  const response = await fetch(url);
  let data = await response.json();
  console.log(data);
  let tableBody = document.getElementById("ShowAllUsers"); // Correctly refer to the table body element

  data.forEach((user) => {
    tableBody.innerHTML += `
                <tr>
          <td>
          <div class="d-flex align-items-center">
          <div class="ms-3">
        <p class="fw-bold mb-1">${user.id}</p>
      </div>
    </div>
  </td>
  <td>
    <p class="text-muted mb-0">${user.name}</p>
  </td>
  <td>
    <span class="">${user.email}</span>
  </td>
  <td>
    <p class="text-muted mb-0">${user.phoneNumber}</p>
  </td>
  <td>
    <p class="text-muted mb-0">${user.address}</p>
  </td>
  <td>
    <button type="button" class="btn btn-link btn-sm btn-rounded">
      Edit
    </button>
  </td>
</tr>`;
  });
}
getData();
