$(document).ready(function () {

});

let cartData = [];
let grandTotal = 0;

$('#paybtn').click(function (e) {
    e.preventDefault();

    // 1. Fetch Cart Items First
    $.ajax({
        url: '/Purchase/GetCart',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            if (data.length === 0) {
                alert("Cart is empty");
                return;
            }

            cartData = data;
            grandTotal = 0;

            $.each(cartData, function (i, item) {
                grandTotal += item.quantity * item.ppu;
            });

            let cgst = grandTotal * 0.05;
            let sgst = grandTotal * 0.05;
            let finalTotal = grandTotal + cgst + sgst;

            let amountInPaise = Math.round(finalTotal * 100); // Razorpay needs in paisa

            const razorOptions = {
                "key": "rzp_test_Kl7588Yie2yJTV",
                "amount": amountInPaise,
                "currency": "INR",
                "name": "Pharma",
                "description": "Supplier Payment",
                "handler": function (response) {
                    alert("✅ Payment Successful!");

                    // --- 1. Add Purchase ---
                    let invoiceNo = "INV-" + new Date().getTime();
                    let purchaseData = {
                        SupplierId: cartData[0].supplierId,
                        PurchaseDate: new Date().toISOString(),
                        InvoiceNo: invoiceNo,
                        TotalAmount: finalTotal,
                    };

                    $.ajax({
                        url: '/Purchase/AddPurchase',
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'Application/x-www-form-urlencoded;charset=utf-8',
                        data: purchaseData,
                        success: function (res1) {
                            if (res1.success) {

                                // --- 2. Add Each Item to PurchaseItem ---
                                let successCount = 0;
                                $.each(cartData, function (i, item) {
                                    let itemData = {
                                        MedicineId: item.medicineId,
                                        Quantity: item.quantity,
                                        CostPrice: parseFloat((item.ppu * 1.05).toFixed(2)),
                                    };

                                    $.ajax({
                                        url: '/Purchase/AddPurchaseItem',
                                        type: 'POST',
                                        dataType: 'json',
                                        contentType: 'Application/x-www-form-urlencoded;charset=utf-8',
                                        data: itemData,
                                        success: function (res2) {
                                            if (res2.success) {
                                                successCount++;
                                                if (successCount === cartData.length) {
                                                    // --- 3. Delete Cart After All Items Added ---
                                                    const cartIds = cartData.map(x => x.cartId);
                                                    $.ajax({
                                                        url: '/Purchase/DeleteCart',
                                                        type: 'POST',
                                                        traditional: true, // important for list
                                                        data: { ids: cartIds },
                                                        success: function (res3) {
                                                            if (res3.success) {
                                                                alert("🎉 Purchase completed and cart cleared!");
                                                                window.location.reload();
                                                            } else {
                                                                alert("Purchase done but cart could not be cleared.");
                                                            }
                                                        },
                                                        error: function () {
                                                            alert("Error deleting cart.");
                                                        }
                                                    });
                                                }
                                            } else {
                                                alert("Error adding PurchaseItem.");
                                            }
                                        }
                                    });
                                });
                            } else {
                                alert("Error saving purchase.");
                            }
                        },
                        error: function () {
                            alert("Failed to add purchase.");
                        }
                    });
                },
                "prefill": {
                    "name": "Krish Kheloji",
                    "email": "khelojikrish@gmail.com",
                    "contact": "7208921898"
                },
                "theme": {
                    "color": "#3399cc"
                }
            };

            let rzp1 = new Razorpay(razorOptions);
            rzp1.open();
        },
        error: function () {
            alert("Failed to fetch cart.");
        }
    });
});
