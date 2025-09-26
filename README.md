# 📱 AM-System: Affiliate Marketing & Delivery Management (Android)

A bilingual Android application designed to streamline affiliate-driven product sales and delivery operations. The system supports multiple roles with distinct permissions, real-time order tracking, commission management, and API integration with delivery services.

---

## 🌐 Platform

- Android mobile application
- Full support for **Arabic** and **English**
- Role-based interface customization

---

## 👥 Roles & Permissions

### 👑 Admin
- Full system access
- Product management (Add/Edit/Delete)
- Manual commission assignment per product
- Affiliate management
- Order oversight
- Delivery API integration
- Approval of commission withdrawal requests

### 🛠 Assistant Admin
- Review and confirm/reject orders
- No access to commission or system settings

### 📢 Affiliate
- Submit new orders (customer + product info)
- Track order status in real-time
- View commissions (total, confirmed, pending)
- Request commission withdrawals
- Receive order-related notifications

### 🚚 Delivery Office
- Receives confirmed orders via API
- Assigns orders to drivers
- Updates order status via delivery system

### 👨‍✈️ Driver
- Views only assigned orders
- Updates order status: In Transit, Delivered, Rejected, No Response

### 📞 Call Center Agent
- Contacts customers via in-app call button
- Updates order status: Confirmed, Rejected, No Response

---

## 🔄 Order Lifecycle

1. Affiliate submits order
2. Assistant Admin reviews and confirms/rejects
3. Confirmed orders sent to Delivery Office via API
4. Delivery Office assigns driver
5. Driver updates delivery status
6. Affiliate sees live status and commission updates

---

## 💰 Commission System

- Fixed commission per product (set by Admin)
- Commission is earned only if:
  - Order is successfully delivered
  - No cancellation, return, or damage
- Affiliate dashboard shows:
  - Total commissions
  - Confirmed commissions
  - Pending commissions

---

## 💸 Commission Withdrawal

- Withdrawal methods: Cash or Postal Transfer
- Affiliate submits withdrawal request
- Admin approves before payout
- Full withdrawal history tracking

---

## 📞 Phone Confirmation

- In-app call button for direct customer contact
- Order status updated post-call:
  - Confirmed
  - Rejected
  - No Response

---

## 📊 Reports & Analytics

- Per-affiliate reports:
  - Top-selling products
  - Orders by status
  - Commission breakdown (total, pending, paid)
- Notifications:
  - Affiliate: Order received / canceled
  - Admin/Assistant: New order / delayed processing

---

## 🌍 Language Support

- Arabic 🇸🇦
- English 🇬🇧
- Toggle available within the app

---

## 🧾 Product Management

- Admin can Add/Edit/Delete products:
  - Type, Price, Commission, Quantity, Description
- Products shown to affiliates during order creation

---

## 🔗 Delivery API Integration

- Integration with a single delivery company
- API handles:
  - Auto-assignment of orders
  - Syncing delivery status (Delivered, In Transit, Rejected)
- Driver app connected to delivery system

---

## 📦 Project Structure
AMSystem/
├── .gitignore
├── AMSystem.sln 
├── src/ # Main application code 
└── tests/ # Unit and integration tests

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 🚀 Getting Started

1. Clone the repo
2. Open in Android Studio
3. Configure API keys and connection strings
4. Build and run on emulator or device

---

## 🤝 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

---

