// Functions for modal handling
function openModal() {
    document.getElementById("myModal").style.display = "block";
    document.getElementById("modalBackdrop").style.display = "block";
}

function closeModal() {
    document.getElementById("myModal").style.display = "none";
    document.getElementById("modalBackdrop").style.display = "none";
}

function highlightCloseIcon(element) {
    element.style.backgroundColor = "red";
}

function unhighlightCloseIcon(element) {
    element.style.backgroundColor = "transparent";
}

// Xử lý sự kiện khi nhấn nút "Tìm"
document.getElementById('searchButton').addEventListener('click', filterRooms);

function filterRooms() {
    var checkinDate = document.getElementById("checkinDate").value;
    var checkoutDate = document.getElementById("checkoutDate").value;
    var adults = parseInt(document.getElementById("adults").value);
    var children = parseInt(document.getElementById("children").value) || 0;

    if (!checkinDate || !checkoutDate || isNaN(adults)) {
        alert("Vui lòng điền đầy đủ thông tin.");
        return;
    }

    if (checkinDate > checkoutDate) {
        alert("Ngày trả phòng không hợp lệ.");
        return;
    }

    var data = {
        checkInDate: checkinDate,
        checkOutDate: checkoutDate,
        adults: adults,
        children: children,
        roomId: document.getElementById("roomId").value
    };

    // Gửi yêu cầu AJAX đến máy chủ
    $.ajax({
        url: '/Hotel/FilterRooms',
        type: 'POST',
        data: data,
        success: function (response) {
            // Xử lý kết quả trả về từ máy chủ
            var tbody = document.querySelector('.room-table tbody');
            tbody.innerHTML = response;
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

// Function to handle form submission
function handleFormSubmission(event) {
    event.preventDefault(); // Ngăn chặn form được gửi đi
    const action = event.submitter.value; // Lấy giá trị của nút được nhấn
    if (action === 'checkoutNow') {
        const formData = new FormData(document.getElementById('bookingForm'));
        const bookingViews = [];
        const quantities = document.querySelectorAll('select[name^="quantity_"]');
        for (let i = 0; i < quantities.length; i++) {
            const quantity = parseInt(quantities[i].value);
            if (quantity > 0) {
                const roomTypeIdInput = document.querySelector(`input[name="roomTypeId_${quantities[i].name.split('_')[1]}"]`);
                const roomTypeNameInput = document.querySelector(`input[name="roomTypeName_${quantities[i].name.split('_')[1]}"]`);
                const roomTypePriceInput = document.querySelector(`input[name="roomTypePrice_${quantities[i].name.split('_')[1]}"]`);

                if (roomTypeIdInput && roomTypeNameInput && roomTypePriceInput) {
                    const roomTypeId = roomTypeIdInput.value;
                    const roomTypeName = roomTypeNameInput.value;
                    const roomTypePrice = roomTypePriceInput.value;
                    const bookingView = {
                        hotelId: modelId, // Thay modelId bằng giá trị thích hợp
                        checkInDate: formData.get('checkInDate'),
                        checkOutDate: formData.get('checkOutDate'),
                        adults: parseInt(formData.get('adults')),
                        children: parseInt(formData.get('children')),
                        roomTypeId: parseInt(roomTypeId),
                        quantity: quantity,
                        totalPrice: parseFloat(roomTypePrice) * quantity
                    };
                    bookingViews.push(bookingView);
                }
            }
        }
        if (bookingViews.length === 0) {
            // Thông báo cho người dùng nếu không có phòng nào được chọn
            console.error('Vui lòng chọn ít nhất một phòng để đặt.');
            return;
        }
        // Chuyển dữ liệu sang chuỗi JSON và mã hóa để truyền qua URL
        const encodedData = encodeURIComponent(JSON.stringify(bookingViews));
        // Chuyển hướng đến trang BookingNow và truyền dữ liệu qua URL
        window.location.href = '/Booking/BookingNow?bookingViews=' + encodedData;
    }
}