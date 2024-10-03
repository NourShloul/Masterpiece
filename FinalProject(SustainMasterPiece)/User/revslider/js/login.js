document
  .getElementById("loginForm")
  .addEventListener("click", async function (event) {
    event.preventDefault(); // منع الإرسال الافتراضي للنموذج

    const formData = new FormData(this); // استخدام FormData لجمع بيانات النموذج

    try {
      const response = await fetch("http://localhost:5036/api/User/Login", {
        // تغيير إلى عنوان API الخاص بك
        method: "POST",
        body: formData,
      });

      if (response.ok) {
        const data = await response.json();
        localStorage.setItem("userId"); // حفظ معرف المستخدم في التخزين المحلي
        alert("Login Successfully");
        window.location.href = "index-4.html"; // إعادة التوجيه إلى صفحة الملف الشخصي
      } else {
        const errorData = await response.json();
        alert(errorData.message); // عرض رسالة الخطأ
      }
    } catch (error) {
      console.error("Error:", error);
      alert("An error occurred during login.");
    }
  });
