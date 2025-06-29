#!/usr/bin/env bash
BASE_URL="http://localhost:5001/api/bookmarks"
APPLICANT_ID=1
JOB_ID=42

echo "1) List existing bookmarks (should be empty):"
curl -i "${BASE_URL}/${APPLICANT_ID}"
echo -e "\n\n"

echo "2) Add a bookmark (POST):"
curl -i -X POST "${BASE_URL}/${APPLICANT_ID}/${JOB_ID}" \
     -H "Accept: application/json"
echo -e "\n\n"

echo "3) List bookmarks again (should contain the one you just added):"
curl -i "${BASE_URL}/${APPLICANT_ID}"
echo -e "\n\n"

echo "4) Remove that bookmark (DELETE):"
curl -i -X DELETE "${BASE_URL}/${APPLICANT_ID}/${JOB_ID}" \
     -H "Accept: application/json"
echo -e "\n\n"

echo "5) List bookmarks again (should be empty):"
curl -i "${BASE_URL}/${APPLICANT_ID}"
echo -e "\n\n"

echo "6) Add two bookmarks:"
curl -i -X POST "${BASE_URL}/${APPLICANT_ID}/100" -H "Accept: application/json"
echo
curl -i -X POST "${BASE_URL}/${APPLICANT_ID}/200" -H "Accept: application/json"
echo -e "\n\n"

echo "7) List bookmarks (should show two entries):"
curl -i "${BASE_URL}/${APPLICANT_ID}"
echo -e "\n\n"

echo "8) Clear all bookmarks (DELETE /clear):"
curl -i -X DELETE "${BASE_URL}/${APPLICANT_ID}/clear" \
     -H "Accept: application/json"
echo -e "\n\n"

echo "9) List bookmarks (final, should be empty):"
curl -i "${BASE_URL}/${APPLICANT_ID}"
echo -e "\n"