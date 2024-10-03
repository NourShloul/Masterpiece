document
  .getElementById("registerLink")
  .addEventListener("click", function (event) {
    event.preventDefault(); // منع التحويل التلقائي للرابط

    // الحصول على البيانات من النموذج
    const form = document.getElementById("registerForm");
    const formData = new FormData(form);

    // تحويل بيانات النموذج إلى كائن
    const data = Object.fromEntries(formData.entries());

    // التحقق من تطابق كلمات المرور
    if (data.password !== data.ConfirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    // إرسال طلب POST إلى الـ API
    fetch("http://localhost:5036/api/User/Register", {
      method: "POST",
      body: formData, // إرسال البيانات كـ FormData
    })
      .then((response) => response.json())
      .then((data) => {
        if (data) {
          alert("Registration successful!");
          // إعادة توجيه المستخدم إلى صفحة تسجيل الدخول
          window.location.href = "login.html";
        }
      })
      .catch((error) => {
        console.error("Error:", error);
        alert("Registration failed, please try again.");
      });
  });
