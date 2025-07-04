import React, { useState } from 'react';

function Emails() {
    const [status, setStatus] = useState('');
    const [email, setEmail] = useState('');

    const handleEmail = async (e) => {
        e.preventDefault();
        const email = e.target.email.value;

        if (!email) {
            setStatus('Please enter an email address.');
            return;
        }

        try {
            const response = await fetch('/api/email/', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ toEmail: email }),
            });

            if (response.ok) {
                setStatus('Email sent successfully!');
            }
            else {
                const errorText = await response.text();
                setStatus(`Failed to send email: ${errorText}`);
            }
        }
        catch (error) {
            setStatus('Error sending email.');
            console.error(error);
        }

    };

    return (
        <div>
            <h2>Email your friends</h2>
            <form onSubmit={handleEmail}>
                <input
                    type="email"
                    name="email"
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                    required
                />
                <button type="submit">Send</button>
            </form>
            {status && <p>{status}</p>}
        </div>
    );
}

export default Emails;
