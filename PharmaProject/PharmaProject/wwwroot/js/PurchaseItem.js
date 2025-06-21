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

            let amountInPaise = Math.round(finalTotal * 100);

            const razorOptions = {
                "key": "rzp_test_Kl7588Yie2yJTV",
                "amount": amountInPaise,
                "currency": "INR",
                "name": "Pharma",
                "description": "Supplier Payment",
                handler: function (response) {
                    alert("✅ Payment Successful!");

                    let invoiceNo = "INV-" + new Date().getTime();

                    $.ajax({
                        url: '/Purchase/GeneratePurchaseInvoicePdf',
                        type: 'POST',
                        contentType: 'application/json',
                        data: JSON.stringify({
                            cartItems: cartData,
                            total: grandTotal,
                            invoiceNo: invoiceNo
                        }),
                        success: function (pdfRes) {
                            if (pdfRes.success) {
                                let invoicePath = pdfRes.path;

                                let purchaseData = {
                                    SupplierId: cartData[0].supplierId,
                                    PurchaseDate: new Date().toISOString(),
                                    InvoiceNo: invoicePath, 
                                    TotalAmount: finalTotal,
                                };

                                $.ajax({
                                    url: '/Purchase/AddPurchase',
                                    type: 'POST',
                                    dataType: 'json',
                                    contentType: 'application/x-www-form-urlencoded;charset=utf-8',
                                    data: purchaseData,
                                    success: function (res1) {
                                        if (res1.success) {

                                            let successCount = 0;
                                            $.each(cartData, function (i, item) {
                                                let itemData = {
                                                    MedicineId: item.medicineId,
                                                    Quantity: item.quantity,
                                                    CostPrice: parseFloat((item.ppu * 1.5).toFixed(2)),
                                                };

                                                $.ajax({
                                                    url: '/Purchase/AddPurchaseItem',
                                                    type: 'POST',
                                                    dataType: 'json',
                                                    contentType: 'application/x-www-form-urlencoded;charset=utf-8',
                                                    data: itemData,
                                                    success: function (res2) {
                                                        if (res2.success) {
                                                            successCount++;
                                                            if (successCount === cartData.length) {

                                                                const cartIds = cartData.map(x => x.cartId);
                                                                $.ajax({
                                                                    url: '/Purchase/DeleteCart',
                                                                    type: 'POST',
                                                                    traditional: true,
                                                                    data: { ids: cartIds },
                                                                    success: function (res3) {
                                                                        if (res3.success) {
                                                                            alert("🎉 Purchase and Invoice saved!");
                                                                            window.location.href = invoicePath;
                                                                        } else {
                                                                            alert("Purchase done but cart couldn't be cleared.");
                                                                        }
                                                                    },
                                                                    error: function () {
                                                                        alert("Error deleting cart.");
                                                                    }
                                                                });
                                                            }
                                                        } else {
                                                            alert("Error adding purchase item.");
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

                            } else {
                                alert("❌ Failed to generate invoice PDF.");
                            }
                        },
                        error: function () {
                            alert("❌ Error in PDF generation request.");
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
