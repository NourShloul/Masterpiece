document
  .getElementById("loginLink")
  .addEventListener("click", async function (event) {
    event.preventDefault(); // Prevent the default button action

    const loginForm = document.getElementById("loginForm");
    const formData = new FormData(loginForm); // Collect form data

    try {
      const response = await fetch("http://localhost:5036/api/User/Login", {
        method: "POST",
        body: formData, // Send FormData directly
      });

      if (response.ok) {
        const data = await response.json();
        localStorage.setItem("userId", data.userId); // Store user ID in local storage
        window.location.href = "index-4.html"; // Redirect to home page or profile
      } else {
        const errorData = await response.json();
        alert(errorData.message); // Show error message
      }
    } catch (error) {
      console.error("Error:", error);
      alert("An error occurred during login.");
    }
  });
